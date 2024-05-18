using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace OrchestrationExample.Services
{
    public class SampleWorkFlow : IWorkflow
    {
        public string Id => "Sample";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<FirstStep>()
                .Then<LastStep>();
        }
    }
}
