using QueryFiltering.Helpers;
using QueryFiltering.Nodes.Base;

namespace QueryFiltering.Nodes.Functions
{
    internal class EndsWithNode : FunctionNode
    {
        public EndsWithNode(BaseNode[] parameters) : base(TypeCashe.String.EndsWith, parameters)
        {
        }
    }
}
