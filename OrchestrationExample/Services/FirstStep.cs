using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace OrchestrationExample.Services
{
    public class FirstStep : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Microservice A");
            return ExecutionResult.Next();
        }
    }
}