﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using Microsoft.Practices.Unity;
using SimpleEventBroker;
using StopLight.ServiceImplementations;
using StopLight.ServiceInterfaces;

namespace StopLight.Logic
{
	public class StoplightSchedule
	{
		private IStoplightTimer timer;
		private ILogger logger = new NullLogger();
		private TimeSpan[] lightTimes = new TimeSpan[3];
		private int currentLight = 0;

        [Publishes("ChangeLight")]
		public event EventHandler ChangeLight;

		public StoplightSchedule(IStoplightTimer timer)
		{
			this.timer = timer;
		}

		[Dependency]
		public ILogger Logger
		{
			get { return logger; }
			set { logger = value; }
		}

		public void Start()
		{
			timer.Start();
		}

		public void Update(TimeSpan green, TimeSpan yellow, TimeSpan red)
		{
			lightTimes[0] = green;
			lightTimes[1] = yellow;
			lightTimes[2] = red;

			logger.Write(string.Format("UPDATE SCHEDULE: {0} {1} {2}", green, yellow, red));
		}

		public void ForceChange()
		{
			OnTimerExpired(this, EventArgs.Empty);
			logger.Write(string.Format("FORCED CHANGE"));
		}

        [SubscribesTo("TimerTick")]
		public void OnTimerExpired(object sender, EventArgs e)
		{
			EventHandler handlers = ChangeLight;
			if(handlers != null)
			{
				handlers(this, EventArgs.Empty);
			}
			currentLight = ( currentLight + 1 ) % 3;
			timer.Duration = lightTimes[currentLight];
			timer.Start();
		}
	}
}
