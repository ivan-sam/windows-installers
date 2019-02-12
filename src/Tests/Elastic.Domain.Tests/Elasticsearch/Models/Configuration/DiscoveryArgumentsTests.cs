﻿using Elastic.Installer.Domain.Model.Elasticsearch.Config;
using FluentAssertions;
using Xunit;

namespace Elastic.Installer.Domain.Tests.Elasticsearch.Models.Configuration
{
	public class DiscoveryArgumentsTests : InstallationModelArgumentsTestsBase
	{
		[Fact] void SeedHostsEmpty() => Argument(nameof(ConfigurationModel.SeedHosts), "", (m, v) =>
		{
			m.ConfigurationModel.SeedHosts.Should().BeEmpty();
		});

		[Fact] void SeedHostsSingle() => Argument(nameof(ConfigurationModel.SeedHosts), "my-domain:9200", (m, v) =>
		{
			m.ConfigurationModel.SeedHosts.Should().NotBeEmpty().And.HaveCount(1).And.Contain(v);
		});

		[Fact] void SeedHostsMultiple() => Argument(
			nameof(ConfigurationModel.SeedHosts),
			"my-domain:9200,,,  192.3.3.1:8080",
			"my-domain:9200,192.3.3.1:8080",
			(m, v) =>
		{
			m.ConfigurationModel.SeedHosts.Should().NotBeEmpty().And.HaveCount(2).And.Contain("my-domain:9200");
		});

		[Fact] void MinimumMasterNodes() => Argument(nameof(ConfigurationModel.MinimumMasterNodes), 2, (m, v) =>
		{
			m.ConfigurationModel.MinimumMasterNodes.Should().Be(2);
			m.ConfigurationModel.IsValid.Should().BeTrue();
		});

		[Fact] void MinimumMasterNodesNegative() => Argument(nameof(ConfigurationModel.MinimumMasterNodes), -1, (m, v) =>
		{
			m.ConfigurationModel.MinimumMasterNodes.Should().Be(-1);
			m.ConfigurationModel.IsValid.Should().BeFalse();
		});

	}
}
