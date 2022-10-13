# IBAN4Net

<p><img src="https://img.shields.io/badge/netstandard-1.2-blue" /> <img src="https://img.shields.io/badge/netstandard-1.4-blue" /> <img src="https://img.shields.io/badge/netstandard-2.0-blue" /> <img src="https://img.shields.io/badge/.net-4.5-blue" /> <img src="https://img.shields.io/badge/.net-5.0-blue" /> <img src="https://img.shields.io/badge/.net-6.0-blue" /></p>
<p><a href="https://www.nuget.org/packages/IBAN4Net"> <img src="https://buildstats.info/nuget/IBAN4Net" /></a></p>

IBAN4Net is a library for validation and generation of the International Bank Account Numbers (IBAN) and Business Identifier Codes (BIC).

This project is a port of Artur Mkrtchyan's IBAN4j project to .NET (netstandard). Artur's project can be found <a href="https://github.com/arturmkrtchyan/iban4j" target="_blank">here.</a>


More information about IBANs and BICs can be found here:

* <a href="http://en.wikipedia.org/wiki/ISO_13616" target="_blank">IBAN ISO_13616</a>
* <a href="http://en.wikipedia.org/wiki/ISO_9362" target="_blank">BIC ISO_9362</a>

This library is written with C# ver. 6.0 and it is not a one-to-one port of the original IBAN4j project. It contains some tweaks here and there and it doesn't support random IBAN generation, yet.

Since 10/2022 the library contains definitions for all countries with IBAN support listed as Partial/Experimental. Definitions for those countries are...vague at best. Take that as a warning. PRs better clarifying those rules are much welcomed.


Some of the differencies from the java project are:

- CountryCode dictionary contains definitions for all of the countries (I hope)
- Instead of valueOf(), I'm using a pattern of CreateInstance()
- Iban Builder verifies lengths of some of the parts during formatting and can left-pad them with zeroes if they are shorter than BBAN rule specifies. This feature is enabled by default, but can be disabled (see examples). 
- Validation of IBAN and BIC can be done without exceptions (see examples)



<b>You can get this library directly to your project from NuGet: <a href="https://www.nuget.org/packages/IBAN4Net/">https://www.nuget.org/packages/IBAN4Net/</a></b>


### Quick examples of usage:

#### IBANs:

```c#
// How to generate IBAN:
Iban iban = new IbanBuilder()
                .CountryCode( CountryCode.GetCountryCode( "CZ" ) )
                .BankCode( "0800" )
                .AccountNumberPrefix( "000019" )
                .AccountNumber( "2000145399" )   
                .Build(); 

// iban.ToString() = CZ6508000000192000145399

// or with autopadding (supplement missing zeroes):
Iban iban = new IbanBuilder()
                .CountryCode( CountryCode.GetCountryCode( "CZ" ) )
                .BankCode( "800" )
                .AccountNumberPrefix( "19" )
                .AccountNumber( "2000145399" )   
                .Build(); 

// iban.ToString() = CZ6508000000192000145399

// disable autopadding:
Iban iban = new IbanBuilder()
                .CountryCode( CountryCode.GetCountryCode( "CZ" ) )
                .BankCode( "800" )
                .AccountNumberPrefix( "19" )
                .AccountNumber( "2000145399" )   
                .Build(true, false); 


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

// Validation of IBAN without throwing an exception:
IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
if (IbanUtils.IsValid("CZ6508000000192000145399", out validationResult))
{
    // valid IBAN
}
else
{
    // invalid IBAN
    // validationResult contains reason of unsuccessful validation
}

// Check if IBAN is from SEPA member country
bool ibanFromSEPA = IbanUtils.IsFromSEPACountry("CZ5508000000001234567899"); // true

// Check if IBAN is from SEPA Eurozone (uses EUR as currency) member country
bool ibanWithEUR = IbanUtils.IsFromEurozoneSEPACountry("FI1410093000123458"); // true

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

// Validation of BIC without throwing an exception:
BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
if (BicUtils.IsValid("DEUTDEFF500", out validationResult))
{
    // valid BIC
}
else
{
    // invalid BIC
    // validationResult contains reason of unsuccessful validation
}


```

### Contribute and PRs

I don't like PRs supporting the everlasting "tabs vs. spaces" battle. Main project contains .editorconfig (VS2022) file which sets the score clearly ;)



I'm really sorry for a rather long delays when accepting PRs. 


### References
- <a href="https://github.com/arturmkrtchyan/iban4j">IBAN4j project by Artur Mkrtchyan</a>
- <a href="http://en.wikipedia.org/wiki/ISO_13616">http://en.wikipedia.org/wiki/ISO_13616</a>
- <a href="http://en.wikipedia.org/wiki/ISO_9362">http://en.wikipedia.org/wiki/ISO_9362</a>
- <a hhref="http://www.swift.com/dsp/resources/documents/IBAN_Registry.pdf">http://www.swift.com/dsp/resources/documents/IBAN_Registry.pdf</a>



### License
Copyright 2022 Vaclav Beca [sinkien].

Licensed under the Apache License, Version 2.0: http://www.apache.org/licenses/LICENSE-2.0
