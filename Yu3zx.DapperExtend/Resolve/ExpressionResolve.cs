using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace Yu3zx.DapperExtend.Resolve
{
    internal class ExpressionResolve
    {
        /// <summary>
        /// 记录参数名和值
        /// </summary>
        public Dictionary<string, object> ParamValues = new Dictionary<string, object>();

        /// <summary>
        /// 参数名称集合
        /// </summary>
        private IEnumerable<string> Parame
        {
            get { return ParamValues.Keys.ToList(); }
        }

        public ModelDes Des { get; set; }

        public string ParamPrefix { get; set; }

        public string BinaryExpressionHandler(Expression left, Expression right, ExpressionType type)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("(");
            const string needParKey = "=,>,<,>=,<=,<>";

            string leftPar = this.RouteExpressionHandler(left, false);
            string typeStr = this.ExpressionTypeCast(type);
            bool needPar = needParKey.IndexOf(typeStr, System.StringComparison.Ordinal) > -1;
            string rightPar = this.RouteExpressionHandler(right, needPar);
            bool or = leftPar.IndexOf('&') > -1 || leftPar.IndexOf('|') > -1 || rightPar.IndexOf('&') > -1 || rightPar.IndexOf('|') > -1;
            needPar = (needParKey.IndexOf(typeStr, System.StringComparison.Ordinal) > -1 && !or);
            string appendLeft = leftPar;
            if (left is MemberExpression)
            {
                var filed = Des.Properties.FirstOrDefault(m => m.Field == leftPar);

                if (filed != null && !string.IsNullOrEmpty(filed.Column))
                {
                    appendLeft = filed.Column;
                }
            }

            sb.Append(appendLeft);

            var paramCount = Parame.Count(p => p.StartsWith(ParamPrefix + leftPar));
            var paramName = ParamPrefix + leftPar + (paramCount > 0 ? paramCount + "" : "");

            if (needPar)
            {
                this.ParamValues.Add(paramName, rightPar);
            }

            if (rightPar.ToUpper() == "NULL")
            {
                switch (typeStr)
                {
                    case "=":
                        rightPar = " IS NULL ";
                        break;
                    case "<>":
                        rightPar = " IS NOT NULL ";
                        break;
                }
            }
            else
            {
                sb.Append(typeStr);
                if (needPar)
                {
                    rightPar = paramName;
                }
            }
            sb.Append(rightPar);
            // sb.Append(")");
            return sb.ToString();
        }

        public string RouteExpressionHandler(Expression exp, bool isRight = false)
        {
            string result = "";
            if (exp is BinaryExpression)
            {
                BinaryExpression be = (BinaryExpression)exp;
                result = this.BinaryExpressionHandler(be.Left, be.Right, be.NodeType);
            }
            else if (exp is MemberExpression)
            {
                MemberExpression mExp = (MemberExpression)exp;
                if (isRight)
                {
                    object obj = Expression.Lambda(mExp, new ParameterExpression[0]).Compile().DynamicInvoke(new object[0]);
                    if (obj is Enum)
                    {
                        obj = (int)obj;
                    }
                    result = string.Concat(obj);
                }
                else
                {
                    result = mExp.Member.Name;
                }
            }
            else if (exp is NewArrayExpression)
            {
                NewArrayExpression naExp = (NewArrayExpression)exp;
                StringBuilder sb = new StringBuilder();
                foreach (Expression expression in naExp.Expressions)
                {
                    sb.AppendFormat(",{0}", this.RouteExpressionHandler(expression, false));
                }
                result = ((sb.Length == 0) ? "" : sb.Remove(0, 1).ToString());
            }
            else if (exp is MethodCallExpression)
            {
                if (isRight)
                {
                    result = string.Concat(Expression.Lambda(exp, new ParameterExpression[0]).Compile().DynamicInvoke(new object[0]));
                }
                else
                {
                   /* MethodAnalyze<TEntity> methodAnalyze = new MethodAnalyze<TEntity>();
                    Dictionary<string, MethodHandler> dic = new Dictionary<string, MethodHandler>();
                    dic.Add("Like", new MethodHandler(methodAnalyze.StringLike));
                    dic.Add("NotLike", new MethodHandler(methodAnalyze.StringNotLike));
                    dic.Add("Contains", new MethodHandler(methodAnalyze.StringContains));
                    dic.Add("Between", new MethodHandler(methodAnalyze.DateTimeBetween));
                    dic.Add("DateDiff", new MethodHandler(methodAnalyze.DateTimeDateDiff));
                    dic.Add("In", new MethodHandler(methodAnalyze.In));
                    dic.Add("NotIn", new MethodHandler(methodAnalyze.NotIn));
                    dic.Add("Substring", new MethodHandler(methodAnalyze.Substring));*/
                    MethodCallExpression mcExp = (MethodCallExpression)exp;
                    string methodName = mcExp.Method.Name;
                  /*  
                    if (!dic.ContainsKey(methodName))
                    {
                        throw new Exception("LamadaQuery不支持方法" + mcExp.Method.Name);
                    }*/
                    List<object> args = new List<object>();
                    string field;
                    if (mcExp.Object == null)
                    {
                        field = this.RouteExpressionHandler(mcExp.Arguments[0], false);
                    }
                    else
                    {
                        field = mcExp.Object.ToString().Split(new char[]
						{
							'.'
						})[1];
                        args.Add(Expression.Lambda(mcExp.Arguments[0], new ParameterExpression[0]).Compile().DynamicInvoke(new object[0]));
                    }
                    if (mcExp.Arguments.Count > 1)
                    {
                        args.Add(Expression.Lambda(mcExp.Arguments[1], new ParameterExpression[0]).Compile().DynamicInvoke(new object[0]));
                    }
                    if (mcExp.Arguments.Count > 2)
                    {
                        args.Add(Expression.Lambda(mcExp.Arguments[2], new ParameterExpression[0]).Compile().DynamicInvoke(new object[0]));
                    }
                    if (mcExp.Arguments.Count > 3)
                    {
                        args.Add(Expression.Lambda(mcExp.Arguments[3], new ParameterExpression[0]).Compile().DynamicInvoke(new object[0]));
                    }
                   // result = dic[methodName](field, ref this.parIndex, new AddParameHandler(this.AddParame), args.ToArray());
                }
            }
            else if (exp is ConstantExpression)
            {
                ConstantExpression cExp = (ConstantExpression)exp;
                if (cExp.Value == null)
                {
                    result = "null";
                }
                else
                {
                    result = cExp.Value.ToString();
                }
            }
            else if (exp is UnaryExpression)
            {
                UnaryExpression ue = (UnaryExpression)exp;
                result = this.RouteExpressionHandler(ue.Operand, isRight);
            }
            else
            {
                result = "";
            }

            return result;
        }

        private void AddParame(string name, object value)
        {
            this.ParamValues.Add(name, value);
        }
        public string ExpressionTypeCast(ExpressionType expType)
        {
            if (expType <= ExpressionType.LessThanOrEqual)
            {
                switch (expType)
                {
                    case ExpressionType.Add:
                    case ExpressionType.AddChecked:
                        {
                            string result = "+";
                            return result;
                        }
                    case ExpressionType.And:
                        {
                            string result = "&";
                            return result;
                        }
                    case ExpressionType.AndAlso:
                        {
                            string result = " AND ";
                            return result;
                        }
                    default:
                        switch (expType)
                        {
                            case ExpressionType.Divide:
                                {
                                    string result = "/";
                                    return result;
                                }
                            case ExpressionType.Equal:
                                {
                                    string result = "=";
                                    return result;
                                }
                            case ExpressionType.GreaterThan:
                                {
                                    string result = ">";
                                    return result;
                                }
                            case ExpressionType.GreaterThanOrEqual:
                                {
                                    string result = ">=";
                                    return result;
                                }
                            case ExpressionType.LessThan:
                                {
                                    string result = "<";
                                    return result;
                                }
                            case ExpressionType.LessThanOrEqual:
                                {
                                    string result = "<=";
                                    return result;
                                }
                        }
                        break;
                }
            }
            else
            {
                switch (expType)
                {
                    case ExpressionType.Multiply:
                    case ExpressionType.MultiplyChecked:
                        {
                            string result = "*";
                            return result;
                        }
                    default:
                        switch (expType)
                        {
                            case ExpressionType.NotEqual:
                                {
                                    string result = "<>";
                                    return result;
                                }
                            case ExpressionType.Or:
                                {
                                    string result = "|";
                                    return result;
                                }
                            case ExpressionType.OrElse:
                                {
                                    string result = " OR ";
                                    return result;
                                }
                            case ExpressionType.Subtract:
                            case ExpressionType.SubtractChecked:
                                {
                                    string result = "-";
                                    return result;
                                }
                        }
                        break;
                }
            }
            throw new InvalidCastException("不支持的运算符");
        }
    }
}
