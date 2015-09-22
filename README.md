# IBAN4Net
A port of Artur Mkrtchyan's IBAN4j project to .NET. Artur's project can be found <a href="https://github.com/arturmkrtchyan/iban4j" target="_blank">here</a>

IBAN4Net is a library for generation and validation of the International Bank Account Numbers (IBAN) and Business Identifier Codes (BIC).

More information about IBANs and BICs can be found here:

* <a href="http://en.wikipedia.org/wiki/ISO_13616" target="_blank">IBAN ISO_13616</a>
* <a href="http://en.wikipedia.org/wiki/ISO_9362" target="_blank">BIC ISO_9362</a>

This library is written in C# ver. 6.0 and it is not a one-to-one port of the original IBAN4j project. It contains some tweaks here and there and it doesn't support random IBAN generation, yet.

Some of the differencies from the java project are:

- CountryCode dictionary contains definitions for all of the countries (I hope)
- Instead of valueOf(), I'm using a pattern of CreateInstance()
- Iban Builder verifies lengths of some of the parts during formatting and can left-pad them with zeroes if they are shorter than BBAN rule specifies (see examples).


<b>You can get this library directly to your project from NuGet: <a href="https://www.nuget.org/packages/IBAN4Net/">https://www.nuget.org/packages/IBAN4Net/</a></b>


### Quick examples of usage:

#### IBANs:

```c#
// How to generate IBAN:
Iban iban = new IbanBuilder()
                .CountryCode( CountryCode.GetCountryCode( "CZ" ) )
                .BankCode( "0800" )
                .AccountNumber( "0000192000145399" )   
                .Build(); 

// iban.ToString() = CZ6508000000192000145399

// or (supplement missing zeroes):
Iban iban = new IbanBuilder()
                .CountryCode( CountryCode.GetCountryCode( "CZ" ) )
                .BankCode( "800" )
                .AccountNumber( "192000145399" )   
                .Build(); 

// iban.ToString() = CZ6508000000192000145399


// How to get an IBAN object from IBAN string:
Iban iban = Iban.CreateInstance( "CZ6508000000192000145399" );

// How to get IBAN string from the IBAN object:
string iban = Iban.CreateInstance( "CZ6508000000192000145399" ).ToString();


// How to validate IBAN:
try
{
    IbanUtils.Validate("CZ6508000000192000145399");
}
catch (IbanFormatException iex)
{
    // invalid
}
```

#### BICs:

```c#
// How to get an BIC object from string:
Bic testBic = Bic.CreateInstance( "DEUTDEFF500" );

// How to validate BIC:
try
{
    BicUtils.Validate("DEUTDEFF500");
}
catch (BicFormatException bex)
{
    // invalid
}

```

### References
- <a href="https://github.com/arturmkrtchyan/iban4j">IBAN4j project by Artur Mkrtchyan</a>
- <a href="http://en.wikipedia.org/wiki/ISO_13616">http://en.wikipedia.org/wiki/ISO_13616</a>
- <a href="http://en.wikipedia.org/wiki/ISO_9362">http://en.wikipedia.org/wiki/ISO_9362</a>
- <a hhref="http://www.swift.com/dsp/resources/documents/IBAN_Registry.pdf">http://www.swift.com/dsp/resources/documents/IBAN_Registry.pdf</a>



### License
Copyright 2015 Vaclav Beca [sinkien].

Licensed under the Apache License, Version 2.0: http://www.apache.org/licenses/LICENSE-2.0