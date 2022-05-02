// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using PSRule.Rules.Azure.Configuration;
using PSRule.Rules.Azure.Pipeline.Output;

namespace PSRule.Rules.Azure.Pipeline
{
    public interface ITemplatePipelineBuilder : IPipelineBuilder
    {
        void Deployment(string deploymentName);

        void ResourceGroup(ResourceGroupOption resourceGroup);

        void Subscription(SubscriptionOption subscription);

        void PassThru(bool passThru);
    }

    internal sealed class TemplatePipelineBuilder : PipelineBuilderBase, ITemplatePipelineBuilder
    {
        private const string OUTPUTFILE_PREFIX = "resources-";
        private const string OUTPUTFILE_EXTENSION = ".json";
        private const string DEPLOYMENTNAME_PREFIX = "export-";

        private string _DeploymentName;
        private bool _PassThru;

        internal TemplatePipelineBuilder(PSRuleOption option)
            : base()
        {
            _DeploymentName = string.Concat(DEPLOYMENTNAME_PREFIX, Guid.NewGuid().ToString().Substring(0, 8));
            Configure(option);
        }

        public void Deployment(string deploymentName)
        {
            if (string.IsNullOrEmpty(deploymentName))
                return;

            _DeploymentName = deploymentName;
        }

        public void ResourceGroup(ResourceGroupOption resourceGroup)
        {
            if (resourceGroup == null)
                return;

            Option.Configuration.ResourceGroup = ResourceGroupOption.Combine(resourceGroup, Option.Configuration.ResourceGroup);
        }

        public void Subscription(SubscriptionOption subscription)
        {
            if (subscription == null)
                return;

            Option.Configuration.Subscription = SubscriptionOption.Combine(subscription, Option.Configuration.Subscription);
        }

        public void PassThru(bool passThru)
        {
            _PassThru = passThru;
        }

        protected override PipelineWriter GetOutput()
        {
            // Redirect to file instead
            return !string.IsNullOrEmpty(Option.Output.Path)
                ? new FileOutputWriter(
                    inner: base.GetOutput(),
                    option: Option,
                    encoding: Option.Output.Encoding.GetEncoding(),
                    path: Option.Output.Path,
                    defaultFile: string.Concat(OUTPUTFILE_PREFIX, _DeploymentName, OUTPUTFILE_EXTENSION),
                    shouldProcess: CmdletContext.ShouldProcess
                )
                : base.GetOutput();
        }

        protected override PipelineWriter PrepareWriter()
        {
            return _PassThru ? base.PrepareWriter() : new JsonOutputWriter(GetOutput(), Option);
        }

        public override IPipeline Build()
        {
            return new TemplatePipeline(PrepareContext(), _DeploymentName);
        }
    }
}