using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token 
{
	public enum LToken { Func, Expression, Definition, Controll, Bracket, Symbol, Paramater };
	public LToken name;
	public string value;
	public List<Token> subTokens;

    public Token(LToken name, string value)
    {
        this.name = name;
        this.value = value;
    }

    public Token(LToken name, string value, List<Token> subTokens)
    {
        this.name = name;
        this.value = value;
        this.subTokens = subTokens;
    }
}
