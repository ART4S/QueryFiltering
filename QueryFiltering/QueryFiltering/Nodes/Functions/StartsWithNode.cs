using QueryFiltering.Helpers;
using QueryFiltering.Nodes.Base;

namespace QueryFiltering.Nodes.Functions
{
    internal class StartsWithNode : FunctionNode
    {
        public StartsWithNode(BaseNode[] parameters) : base(TypeCashe.String.StartsWith, parameters)
        {
        }
    }
}
