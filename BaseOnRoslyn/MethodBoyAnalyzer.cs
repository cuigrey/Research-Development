using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace BaseOnRoslyn
{
    public class MethodBoyAnalyzer
    {
        protected List<ReferenceInfo> references = null;
        protected SyntaxList<UsingDirectiveSyntax> usings;

        public List<T> Parse<T>(string codeFilePath, string methodName, int paramsCount)
        {
            string sCodeLocation = codeFilePath;
            string sCodeText = File.ReadAllText(sCodeLocation, Encoding.UTF8);

            FileInfo CSProjFile = null;

            #region Find project file.
            FileInfo codeFileInfo = new FileInfo(sCodeLocation);
            FileInfo[] projFileInfos = codeFileInfo.Directory.GetFiles("*.csproj", SearchOption.TopDirectoryOnly);
            projFileInfos = projFileInfos.Where((projFileInfo) => { if (Regex.IsMatch(projFileInfo.FullName, ".*[.]csproj$")) { return true; } return false; }).ToArray();

            DirectoryInfo parentFolder = null;
            int iDeepth = 0;
            while (!projFileInfos.Any())
            {
                iDeepth++;
                parentFolder = codeFileInfo.Directory.Parent;

                projFileInfos = codeFileInfo.Directory.GetFiles("*.csproj", SearchOption.TopDirectoryOnly);
                projFileInfos = projFileInfos.Where((projFileInfo) => { if (Regex.IsMatch(projFileInfo.FullName, ".*[.]csproj$")) { return true; } return false; }).ToArray();

                if (iDeepth > 3 && codeFileInfo.Directory.GetFiles("*.sln", SearchOption.TopDirectoryOnly).Any())
                {
                    break;
                }
            }

            if (projFileInfos.Any())
            {
                CSProjFile = projFileInfos[0];
            }
            else
            {
                throw new FileNotFoundException(string.Format("Can't found the project configuration file. Code file {0}.", codeFilePath));
            }
            #endregion

            #region Get all references
            references = new List<ReferenceInfo>();

            XDocument xdocCSProj = XDocument.Load(CSProjFile.FullName);
            string sXmlns = xdocCSProj.Root.Attribute(XName.Get("xmlns")).Value;
            List<XElement> refNodes = xdocCSProj.Root.Descendants().Where((xe) => { if (xe.Name.LocalName == "Reference") { return true; } return false; }).ToList();

            ReferenceInfo refInfo = null;
            foreach (XElement refNode in refNodes)
            {
                refInfo = new ReferenceInfo();
                refInfo.IsProjectReference = false;
                refInfo.IsTeamCode = true;
                refInfo.Name = refNode.Attribute(XName.Get("Include")).Value;
                if (refNode.HasElements)
                {
                    refInfo.AssemblyLocationForDebug = refNode.Element(XName.Get("HintPath", sXmlns)).Value;
                    refInfo.AssemblyLocationForDebug = Path.Combine(CSProjFile.Directory.FullName, refInfo.AssemblyLocationForDebug);

                    refInfo.AssemblyLocationForRelease = refNode.Element(XName.Get("HintPath", sXmlns)).Value;
                    refInfo.AssemblyLocationForRelease = Path.Combine(CSProjFile.Directory.FullName, refInfo.AssemblyLocationForDebug);
                }
                if (refInfo.AssemblyLocationForDebug == null)
                {
                    refInfo.AssemblyName = refInfo.Name;

                    refInfo.AssemblyLocationForDebug = refInfo.Name;
                    refInfo.AssemblyLocationForRelease = refInfo.Name;
                }
                if (!File.Exists(refInfo.Name))
                {
                    refInfo.IsTeamCode = false;
                }
                else
                {
                    refInfo.LoadedAssembly = Assembly.LoadFile(refInfo.AssemblyLocationForRelease);
                }
                references.Add(refInfo);
            }

            refNodes = xdocCSProj.Root.Descendants().Where((xe) => { if (xe.Name.LocalName == "ProjectReference") { return true; } return false; }).ToList();
            FileInfo refProjFileInfo = null;
            XDocument xdocRefProjFile = null;
            string sRefCSProjXmlns = string.Empty;
            XElement CSProjInfoNode = null;
            foreach (XElement refNode in refNodes)
            {
                refInfo = new ReferenceInfo();
                refInfo.IsProjectReference = true;
                refInfo.IsTeamCode = true;
                refInfo.Name = refNode.Attribute(XName.Get("Include")).Value;

                refProjFileInfo = new FileInfo(Path.Combine(CSProjFile.Directory.FullName, refInfo.Name));
                xdocRefProjFile = XDocument.Load(refProjFileInfo.FullName);

                sRefCSProjXmlns = xdocRefProjFile.Root.Attribute(XName.Get("xmlns")).Value;
                CSProjInfoNode = xdocRefProjFile.Root.Descendants(XName.Get("AssemblyName", sRefCSProjXmlns)).FirstOrDefault();
                if (CSProjInfoNode != null)
                {
                    refInfo.AssemblyName = CSProjInfoNode.Value;
                }

                CSProjInfoNode = xdocRefProjFile.Root.Descendants(XName.Get("Configuration", sRefCSProjXmlns)).FirstOrDefault();
                if (CSProjInfoNode.Value == "Debug")
                {
                    #region For Debug
                    CSProjInfoNode = xdocRefProjFile.Root.Descendants(XName.Get("PropertyGroup", sRefCSProjXmlns))
                    .Where(xe =>
                    {
                        try
                        {
                            if (xe.Attribute(XName.Get("Condition")).Value.Contains("Debug"))
                            {
                                return true;
                            }
                        }
                        catch
                        {
                        }
                        return false;
                    })
                    .FirstOrDefault();
                    CSProjInfoNode = CSProjInfoNode != null ? CSProjInfoNode.Descendants(XName.Get("OutputPath", sRefCSProjXmlns)).FirstOrDefault() : null;
                    if (CSProjInfoNode != null)
                    {
                        refInfo.AssemblyLocationForDebug = CSProjInfoNode.Value;
                        refInfo.AssemblyLocationForDebug = Path.Combine(refProjFileInfo.Directory.FullName, refInfo.AssemblyLocationForDebug);
                        if (!refInfo.AssemblyLocationForDebug.EndsWith(refInfo.AssemblyName + ".dll"))
                        {
                            refInfo.AssemblyLocationForDebug = Path.Combine(refInfo.AssemblyLocationForDebug, refInfo.AssemblyName + ".dll");
                        }
                    }
                    #endregion
                }
                else
                {
                    #region For Release
                    CSProjInfoNode = xdocRefProjFile.Root.Descendants(XName.Get("PropertyGroup", sRefCSProjXmlns))
                        .Where(xe =>
                    {
                        try
                        {
                            if (xe.Attribute(XName.Get("Condition")).Value.Contains("Release"))
                            {
                                return true;
                            }
                        }
                        catch
                        {
                        }
                        return false;
                    })
                        .FirstOrDefault();
                    CSProjInfoNode = CSProjInfoNode != null ? CSProjInfoNode.Descendants(XName.Get("OutputPath", sRefCSProjXmlns)).FirstOrDefault() : null;
                    if (CSProjInfoNode != null)
                    {
                        refInfo.AssemblyLocationForRelease = CSProjInfoNode.Value;
                        refInfo.AssemblyLocationForRelease = Path.Combine(refProjFileInfo.Directory.FullName, refInfo.AssemblyLocationForRelease);
                        if (!refInfo.AssemblyLocationForRelease.EndsWith(refInfo.AssemblyName + ".dll"))
                        {
                            refInfo.AssemblyLocationForRelease = Path.Combine(refInfo.AssemblyLocationForRelease, refInfo.AssemblyName + ".dll");
                        }
                    }
                    #endregion
                }

                refInfo.Name = refNode.Element(XName.Get("Name", sXmlns)).Value;

                if (File.Exists(refInfo.Name))
                {
                    refInfo.IsTeamCode = false;
                }
                else
                {
                    refInfo.LoadedAssembly = Assembly.LoadFile((refInfo.AssemblyLocationForDebug != null) ? refInfo.AssemblyLocationForDebug : refInfo.AssemblyLocationForRelease);
                }
                references.Add(refInfo);
            }
            #endregion

            CSharpCompilationOptions co = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

            List<SyntaxTree> trees = new List<SyntaxTree>();
            trees.Add(CSharpSyntaxTree.ParseText(sCodeText)); 

            
             CSharpCompilation compilation = CSharpCompilation.Create(@"Roslyn_Output.dll"
                , options: co
                , syntaxTrees: trees);

            #region Add reference metadata to compiler.
            MetadataReference mr = null;
            foreach (ReferenceInfo RI in references)
            {
                if (File.Exists(RI.AssemblyLocationForDebug))
                {
                    if (string.IsNullOrEmpty(RI.AssemblyLocationForRelease))
                    {
                        mr = MetadataReference.CreateFromFile(RI.AssemblyLocationForDebug, properties: MetadataReferenceProperties.Assembly);
                    }
                    else
                    {
                        mr = MetadataReference.CreateFromFile(RI.AssemblyLocationForDebug, properties: MetadataReferenceProperties.Assembly);
                    }
                }
                RI.CurrentReferenceMetadata = mr;
                compilation = compilation.AddReferences(RI.CurrentReferenceMetadata);
            }
            #endregion

            #region create compilation unit and 

            SemanticModel semanticModel = compilation.GetSemanticModel(trees[0]);

            
            CompilationUnitSyntax compilationUnit = trees[0].GetCompilationUnitRoot();

            this.usings = compilationUnit.Usings;

            NamespaceDeclarationSyntax namespaceDeclaration = compilationUnit.Members
                .OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
            ClassDeclarationSyntax classDeclaration = namespaceDeclaration.Members
                .OfType<ClassDeclarationSyntax>().FirstOrDefault();

            MethodDeclarationSyntax methodDeclaration = classDeclaration.Members
                .OfType<MethodDeclarationSyntax>()
                .Where(mds
                    => string.Equals(mds.Identifier.ValueText, methodName) && mds.ParameterList.Parameters.Count >= paramsCount)
                .FirstOrDefault();

            ParameterSyntax parameter = methodDeclaration.ParameterList.Parameters[0];
            #endregion

            //(methodDeclaration.Body.Statements[1] as StatementSyntax);

            //MethodSymbol members = semanticModel.GetDeclaredSymbol(methodDeclaration);
            //// (line - column) information - start at 0.
            //string lcInfo = members.Locations[0].GetLineSpan(true).ToString();

            //Symbol symbolAt44 = semanticModel.LookupSymbols(compilationUnit.DescendantTokens().ToArray()[44].FullSpan.End).FirstOrDefault();

            List<InvocationExpressionSyntax> methodCalls = methodDeclaration.Body.DescendantNodes()
                .OfType<InvocationExpressionSyntax>().ToList();

            

            string sMethodName = (methodCalls[3].Expression as MemberAccessExpressionSyntax).GetLastToken().ValueText;
            ExpressionSyntax es0 = methodDeclaration
                .FindToken((methodCalls[3] as InvocationExpressionSyntax).Expression.FullSpan.Start)
                .Parent as ExpressionSyntax;
            string varType0 = es0.Kind().ToString();// IdentifierName

            if (es0.Kind() == SyntaxKind.IdentifierName)
            {
                SyntaxToken[] callSeq = (methodCalls[3].Expression as MemberAccessExpressionSyntax).DescendantTokens()
                    .Where(st => { if (st.Kind() == SyntaxKind.IdentifierToken) { return true; } return false; }).ToArray();

                SymbolInfo symbolInfo0 = semanticModel.GetSymbolInfo(es0);
                Microsoft.CodeAnalysis.TypeInfo ti = semanticModel.GetTypeInfo(es0);
                string sVariableTypeName = ti.Type.Name; // first token type name. cpb1

                Type tCPB = SearchTypeInAllAssemblies(sVariableTypeName);
                
                MemberInfo memberInfo = null;
                Type sNextType = null;
                int iSearchStart = 1;
                for (; iSearchStart < callSeq.Length; iSearchStart++)
                {
                    memberInfo = tCPB.GetMembers()
                        .Where(mi =>
                        {
                            if (string.Equals(mi.Name, callSeq[iSearchStart].ValueText))
                            {
                                return true;
                            }
                            return false;
                        })
                        .FirstOrDefault();

                    if (memberInfo != null)
                    {
                        switch (memberInfo.MemberType)
                        {
                            case MemberTypes.Field:
                                sNextType = (memberInfo as FieldInfo).FieldType;
                                break;
                            case MemberTypes.Property:
                                sNextType = (memberInfo as PropertyInfo).PropertyType;
                                break;
                            case MemberTypes.Method:
                                sNextType = (memberInfo as MethodInfo).ReturnType;
                                break;
                        }
                        tCPB = sNextType;
                        sNextType = null;
                    }
                    else
                    {
                        break;
                    }
                }

                // Most possibility is extension method. 
                if (memberInfo == null)
                {
                }

                MethodInfo target = memberInfo as MethodInfo;
            }

            foreach (ReferenceInfo RI in references.Where(e => e.LoadedAssembly != null))
            {
                RI.LoadedAssembly = null;
            }

            return null;
        }

        protected Type SearchTypeInAllAssemblies(string className)
        {
            Type tt = null;
            foreach (UsingDirectiveSyntax UDS in usings)
            {
                foreach (ReferenceInfo RI in references.Where(e => e.LoadedAssembly != null && e.IsTeamCode))
                {
                    tt = RI.LoadedAssembly.GetType(UDS.Name.ToString() + "." + className);
                    if (tt != null)
                    {
                        break;
                    }
                }
                if (tt != null)
                {
                    break;
                }
            }
            return tt;
        }

        protected Type SearchTypeInAllAssemblies(string className, string namespaceName = "")
        {
            if (string.IsNullOrEmpty(namespaceName) || string.IsNullOrWhiteSpace(namespaceName))
            {
                return null;
            }

            Type tt = null;
            foreach (ReferenceInfo RI in references.Where(e => e.LoadedAssembly != null && e.IsTeamCode))
            {
                tt = RI.LoadedAssembly.GetType(namespaceName + "." + className);
                if (tt != null)
                {
                    break;
                }
            }
            return tt;
        }

        protected List<MemberInfo> SearchMemberDefinition(string memberName, MemberTypes memberType = MemberTypes.Property, string[] parameterTypeList = null)
        {
            List<MemberInfo> memberInfos = new List<MemberInfo>();
            foreach (ReferenceInfo RI in references.Where(e => e.LoadedAssembly != null))
            {
                RI.LoadedAssembly.GetTypes()
                    .Where(t =>
                {
                    MemberInfo memberInfo = null;
                    if (memberType == MemberTypes.Method)
                    {
                        memberInfo = t.GetMethod(memberName);
                    }
                    else if (memberType == MemberTypes.Property)
                    {
                        memberInfo = t.GetProperty(memberName);
                    }
                    else
                    {
                        memberInfo = t.GetField(memberName);
                    }
                    if (memberInfo != null)
                    {
                        return true;
                    }
                    return false;
                });
            }
            return memberInfos;
        }
    }
}
