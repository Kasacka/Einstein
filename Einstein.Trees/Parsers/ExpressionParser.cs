using System;
using Einstein.Trees.Trees;

namespace Einstein.Trees.Parsers
{
    internal static class ExpressionParser
    {
        public static ExpressionTree Parse(TokenReader reader)
        {
            var expression = ParseTerm(reader);
            while (reader.IsOperator('+') || reader.IsOperator('-'))
            {
                var @operator = reader.Value;
                reader.Skip();
                var rightHandSide = ParseTerm(reader);
                expression = CreateBinaryOperatorExpression(expression, rightHandSide, @operator);
            }
            return expression;
        }

        private static ExpressionTree ParseTerm(TokenReader reader)
        {
            var expression = ParseFactor(reader);
            while (reader.IsOperator('*') || reader.IsOperator('/'))
            {
                var @operator = reader.Value;
                reader.Skip();
                var rightHandSide = ParseFactor(reader);
                expression = CreateBinaryOperatorExpression(expression, rightHandSide, @operator);
            }
            return expression;
        }

        private static ExpressionTree ParseFactor(TokenReader reader)
        {
            if (IsLiteral(reader))
                return ParseLiteralExpression(reader);
            else if (reader.IsOperator('('))
                return ParseEnclosedExpression(reader);
            else
                throw new Exception();
        }

        private static ExpressionTree ParseEnclosedExpression(TokenReader reader)
        {
            reader.ExpectOperator('(');
            var expression = Parse(reader);
            reader.ExpectOperator(')');
            return expression;
        }

        private static ExpressionTree ParseLiteralExpression(TokenReader reader)
        {
            if (IsBooleanLiteral(reader))
                return ParseBooleanLiteralExpression(reader);
            else if (IsNumberLiteral(reader))
                return ParseNumberLiteralExpression(reader);
            else
                throw new Exception();
        }

        private static ExpressionTree ParseBooleanLiteralExpression(TokenReader reader)
        {
            var value = reader.Value;
            reader.Skip();
            return CreateLiteral(LiteralType.Boolean, value);
        }

        private static ExpressionTree ParseNumberLiteralExpression(TokenReader reader)
        {
            var value = reader.Value;
            reader.Skip();
            return CreateLiteral(LiteralType.Number, value);
        }

        private static ExpressionTree CreateBinaryOperatorExpression(ExpressionTree leftHandSide, ExpressionTree rightHandSide, string @operator) =>
            new BinaryOperatorExpressionTree
            {
                LeftHandSide = leftHandSide,
                RightHandSide = rightHandSide,
                Type = ToOperatorType(@operator)
            };

        private static bool IsLiteral(TokenReader reader) =>
            IsBooleanLiteral(reader) || IsNumberLiteral(reader);

        private static bool IsBooleanLiteral(TokenReader reader) => 
            reader.IsKeyword("yes") || reader.IsKeyword("no");

        private static bool IsNumberLiteral(TokenReader reader) =>
            reader.IsNumber();

        private static LiteralExpressionTree CreateLiteral(LiteralType type, string value) =>
            new()
            {
                Type = type,
                Value = value
            };

        private static OperatorType ToOperatorType(string @operator) =>
            @operator switch
            {
                "+" => OperatorType.Plus,
                "-" => OperatorType.Minus,
                "*" => OperatorType.Multiply,
                "/" => OperatorType.Divide,
                _ => throw new NotImplementedException(),
            };

    }
}
