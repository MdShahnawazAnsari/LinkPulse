namespace Link.Domain.Exceptions;


public class LinkNotFoundException : Exception
{
    public LinkNotFoundException(string code) : base($"Link with shortcode '{code}' was not found.")
    {
    }
}

public class UrlExpiredException : Exception
{
    public UrlExpiredException(string code) : base($"Link with shortcode '{code}' has expired.")
    {
    }
}

public class DomainNameNotFoundException : Exception
{
    public DomainNameNotFoundException() : base("Domain Name configuration was accessed before being initialized.") { }

}

public class MultipleTryToSetDomainNameException : Exception
{
    public MultipleTryToSetDomainNameException() : base("Domain Name configuration is already intilization") { }
}