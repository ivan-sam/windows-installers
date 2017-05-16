﻿using Elastic.Installer.Domain.Elasticsearch.Model;
using Elastic.Installer.Domain.Elasticsearch.Model.Tasks;
using Elastic.Installer.Domain.Session;
using Elastic.Installer.Msi.CustomActions;
using Elastic.Installer.Msi.Elasticsearch.CustomActions.Install;
using Microsoft.Deployment.WindowsInstaller;
using WixSharp;

namespace Elastic.Installer.Msi.Elasticsearch.CustomActions.Rollback
{
	public class ElasticsearchRollbackServiceAction : RollbackCustomAction<Elasticsearch>
	{
		public override string Name => nameof(ElasticsearchRollbackServiceAction);
		public override int Order => (int)ElasticsearchCustomActionOrder.RollbackService;
		public override When When => When.Before;
		public override Step Step => new Step(nameof(ElasticsearchServiceInstallAction));

		[CustomAction]
		public static ActionResult ElasticsearchRollbackService(Session session) =>
			session.Handle(() => new UninstallServiceTask(session.ToSetupArguments(ElasticsearchArgumentParser.AllArguments), session.ToISession()).Execute());
	}
}
