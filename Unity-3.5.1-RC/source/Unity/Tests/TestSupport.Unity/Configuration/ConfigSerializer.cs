﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Configuration;
using System.IO;
using SysConfiguration = System.Configuration.Configuration;

namespace Microsoft.Practices.Unity.TestSupport.Configuration
{
    public class ConfigSerializer
    {
        private readonly string filename;

        public ConfigSerializer(string filename)
        {
            this.filename = filename;
        }

        public void Save(string sectionName, ConfigurationSection section)
        {
            DeleteFileIfExists();

            var filemap = new ExeConfigurationFileMap()
                              {
                                  ExeConfigFilename = filename
                              };
            SysConfiguration configuration = ConfigurationManager.OpenMappedExeConfiguration(filemap,
                                                                                             ConfigurationUserLevel.None);

            if (configuration.GetSection(sectionName) != null)
            {
                configuration.Sections.Remove(sectionName);
            }
            configuration.Sections.Add(sectionName, section);
            configuration.Save();
        }

        public SysConfiguration Load()
        {
            var filemap = new ExeConfigurationFileMap { ExeConfigFilename = filename };
            return ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
        }

        private void DeleteFileIfExists()
        {
            string fullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            File.Delete(fullName);
        }
    }
}
