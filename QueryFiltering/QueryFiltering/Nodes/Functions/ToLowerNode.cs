using QueryFiltering.Helpers;
using QueryFiltering.Nodes.Base;

namespace QueryFiltering.Nodes.Functions
{
    internal class ToLowerNode : FunctionNode
    {
        public ToLowerNode(BaseNode[] parameters) : base(TypeCashe.String.ToLower, parameters)
        {
        }
    }
}
