﻿using Elastic.Installer.Domain.Session;
using System;
using System.IO.Abstractions;

namespace Elastic.Installer.Domain.Kibana.Model.Tasks
{
	public class SetEnvironmentVariablesTask : KibanaInstallationTask
	{
		public SetEnvironmentVariablesTask(string[] args, ISession session) : base(args, session) { }
		public SetEnvironmentVariablesTask(KibanaInstallationModel model, ISession session, IFileSystem fileSystem)
			: base(model, session, fileSystem) { }

		protected override bool ExecuteTask()
		{
			this.Session.SendActionStart(1000, ActionName, "Setting environment variables", "[1]");

			var installDirectory = this.InstallationModel.LocationsModel.InstallDir;
			var configDirectory = this.InstallationModel.LocationsModel.ConfigDirectory;

			var state = this.InstallationModel.KibanaEnvironmentState;
			state.SetKibanaHomeEnvironmentVariable(installDirectory);
			state.SetKibanaConfigEnvironmentVariable(configDirectory);
			this.Session.SendProgress(1000, "Environment variables set");
			return true;
		}
	}
}