using System.IO;
using GeminiLab.Autoproj.Evaluators;
using GeminiLab.Autoproj.Handlers;
using GeminiLab.Autoproj.Processors;

namespace GeminiLab.Autoproj.Components {
    // this component contains:
    // - variable definition
    // - variable evaluation
    public class VariableComponent : IExpressionEvaluator, ICommandHandler {
        private const string Category = "variable";

        public bool TryEvaluate(out string result, ProcessorEnvironment env, string command, params string[] param) {
            if (env.TryFindStorageRecursively<string>(Category, command, out var item) && item.Exists) {
                // item.Exists in condition seems to be redundant, but just keep it here
                result = item.Value;
                return true;
            }

            result = null;
            return false;
        }

        public void Handle(string command, string[] param, ProcessorEnvironment env, TextWriter output) {
            if (command == "def" && param.Length >= 1) {
                var key = param[0];
                var value = param.Length >= 2 ? param[1] : "";

                env.OpenStorage<string>(Category, key).Value = value;
            }
        }
    }
}