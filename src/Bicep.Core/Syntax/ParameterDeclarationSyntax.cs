// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Bicep.Core.Navigation;
using Bicep.Core.Parser;

namespace Bicep.Core.Syntax
{
    public class ParameterDeclarationSyntax : SyntaxBase, IDeclarationSyntax
    {
        public ParameterDeclarationSyntax(Token parameterKeyword, IdentifierSyntax name, TypeSyntax type, SyntaxBase? modifier, Token? newLine)
        {
            AssertKeyword(parameterKeyword, nameof(parameterKeyword), LanguageConstants.ParameterKeyword);
            AssertSyntaxType(modifier, nameof(modifier), typeof(ParameterDefaultValueSyntax), typeof(ObjectSyntax));
            AssertTokenType(newLine, nameof(newLine), TokenType.NewLine);

            this.ParameterKeyword = parameterKeyword;
            this.Name = name;
            this.Type = type;
            this.Modifier = modifier;
            this.NewLine = newLine;
        }

        public Token ParameterKeyword { get; }
        
        public IdentifierSyntax Name { get; }

        public TypeSyntax Type { get; }

        // This is a modifier of the parameter and not a modifier of the type
        public SyntaxBase? Modifier { get; }

        public Token? NewLine { get; }

        public override void Accept(SyntaxVisitor visitor)
            => visitor.VisitParameterDeclarationSyntax(this);

        public override TextSpan Span => TextSpan.Between(this.ParameterKeyword, TextSpan.LastNonNull(Type, Modifier, NewLine));
    }
}
