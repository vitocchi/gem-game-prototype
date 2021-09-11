using System;
using System.Text.RegularExpressions;

public class EthereumAddress
{
    private const string REGEX = "^0x[a-fA-F0-9]{40}$";
    public readonly String Value;

    public EthereumAddress(string value)
    {
        if (Regex.IsMatch(value, REGEX))
        {
            Value = value.ToLower();
        }
        else
        {
            throw new ArgumentException("invalid ethereum address");
        }
    }
}