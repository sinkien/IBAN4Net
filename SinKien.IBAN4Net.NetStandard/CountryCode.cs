/*
 * IBAN4Net
 * Copyright 2018 Vaclav Beca [sinkien]
 *
 * Based on Artur Mkrtchyan's project IBAN4j (https://github.com/arturmkrtchyan/iban4j).
 *
 *
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using System.Linq;

namespace SinKien.IBAN4Net
{
    public class CountryCode
    {
        /// <summary>
        /// A list of all supported country codes
        /// ISO 3166-1
        ///         
        /// </summary>
        private SortedDictionary<string, CountryCodeEntry> _alpha3Map = new SortedDictionary<string, CountryCodeEntry>();

        public CountryCode()
        {
            loadMap();
        }

        /// <summary>
        /// Gets CountryCode object from map
        /// </summary>
        /// <param name="code">2 or 3 letters code for country</param>
        /// <returns>Found CountryCodeItem object or null if it is not found</returns>
        public static CountryCodeEntry GetCountryCode(string code)
        {
            CountryCodeEntry result = null;
            CountryCode cc = new CountryCode();

            if (!string.IsNullOrEmpty(code))
            {
                switch (code.Length)
                {
                    case 2:
                        result = cc.getByAlpha2(code.ToUpper());
                        break;
                    case 3:
                        result = cc.getByAlpha3(code.ToUpper());
                        break;
                }
            }

            return result;
        }

        private CountryCodeEntry getByAlpha2(string code)
        {
            CountryCodeEntry result = null;

            if (_alpha3Map != null)
            {
                if (_alpha3Map.ContainsKey(code))
                {
                    result = _alpha3Map[code];
                }
            }

            return result;
        }

        private CountryCodeEntry getByAlpha3(string code)
        {
            CountryCodeEntry result = null;

            if (_alpha3Map != null)
            {
                result = _alpha3Map.Values.Where(x => x.Alpha3.Equals(code)).SingleOrDefault();
            }

            return result;
        }

        private void loadMap()
        {
            _alpha3Map.Add("AF", new CountryCodeEntry() { Alpha2 = "AF", Alpha3 = "AFG", CountryName = "Afghanistan" });
            _alpha3Map.Add("AX", new CountryCodeEntry() { Alpha2 = "AX", Alpha3 = "ALA", CountryName = "Åland Islands" });
            _alpha3Map.Add("AL", new CountryCodeEntry() { Alpha2 = "AL", Alpha3 = "ALB", CountryName = "Albania" });
            _alpha3Map.Add("DZ", new CountryCodeEntry() { Alpha2 = "DZ", Alpha3 = "DZA", CountryName = "Algeria" });
            _alpha3Map.Add("AS", new CountryCodeEntry() { Alpha2 = "AS", Alpha3 = "ASM", CountryName = "American Samoa" });
            _alpha3Map.Add("VI", new CountryCodeEntry() { Alpha2 = "VI", Alpha3 = "VIR", CountryName = "Virgin Islands (U.S.)" });
            _alpha3Map.Add("AD", new CountryCodeEntry() { Alpha2 = "AD", Alpha3 = "AND", CountryName = "Andorra" });
            _alpha3Map.Add("AO", new CountryCodeEntry() { Alpha2 = "AO", Alpha3 = "AGO", CountryName = "Angola" });
            _alpha3Map.Add("AI", new CountryCodeEntry() { Alpha2 = "AI", Alpha3 = "AIA", CountryName = "Anguilla" });
            _alpha3Map.Add("AQ", new CountryCodeEntry() { Alpha2 = "AQ", Alpha3 = "ATA", CountryName = "Antarctica" });
            _alpha3Map.Add("AG", new CountryCodeEntry() { Alpha2 = "AG", Alpha3 = "ATG", CountryName = "Antigua and Barbuda" });
            _alpha3Map.Add("AR", new CountryCodeEntry() { Alpha2 = "AR", Alpha3 = "ARG", CountryName = "Argentina" });
            _alpha3Map.Add("AM", new CountryCodeEntry() { Alpha2 = "AM", Alpha3 = "ARM", CountryName = "Armenia" });
            _alpha3Map.Add("AN", new CountryCodeEntry() { Alpha2 = "AN", Alpha3 = "ANT", CountryName = "Netherlands Antilles" });
            _alpha3Map.Add("AW", new CountryCodeEntry() { Alpha2 = "AW", Alpha3 = "ABW", CountryName = "Aruba" });
            _alpha3Map.Add("AU", new CountryCodeEntry() { Alpha2 = "AU", Alpha3 = "AUS", CountryName = "Australia" });
            _alpha3Map.Add("AZ", new CountryCodeEntry() { Alpha2 = "AZ", Alpha3 = "AZE", CountryName = "Azerbaijan" });
            _alpha3Map.Add("BS", new CountryCodeEntry() { Alpha2 = "BS", Alpha3 = "BHS", CountryName = "Bahamas" });
            _alpha3Map.Add("BH", new CountryCodeEntry() { Alpha2 = "BH", Alpha3 = "BHR", CountryName = "Bahrain" });
            _alpha3Map.Add("BD", new CountryCodeEntry() { Alpha2 = "BD", Alpha3 = "BGD", CountryName = "Bangladesh" });
            _alpha3Map.Add("BB", new CountryCodeEntry() { Alpha2 = "BB", Alpha3 = "BRB", CountryName = "Barbados" });
            _alpha3Map.Add("BE", new CountryCodeEntry() { Alpha2 = "BE", Alpha3 = "BEL", CountryName = "Belgium" });
            _alpha3Map.Add("BZ", new CountryCodeEntry() { Alpha2 = "BZ", Alpha3 = "BLZ", CountryName = "Belize" });
            _alpha3Map.Add("BY", new CountryCodeEntry() { Alpha2 = "BY", Alpha3 = "BLR", CountryName = "Belarus" });
            _alpha3Map.Add("BJ", new CountryCodeEntry() { Alpha2 = "BJ", Alpha3 = "BEN", CountryName = "Benin" });
            _alpha3Map.Add("BM", new CountryCodeEntry() { Alpha2 = "BM", Alpha3 = "BMU", CountryName = "Bermuda" });
            _alpha3Map.Add("BT", new CountryCodeEntry() { Alpha2 = "BT", Alpha3 = "BTN", CountryName = "Bhutan" });
            _alpha3Map.Add("BO", new CountryCodeEntry() { Alpha2 = "BO", Alpha3 = "BOL", CountryName = "Bolivia, Plurinational State of" });
            _alpha3Map.Add("BQ", new CountryCodeEntry() { Alpha2 = "BQ", Alpha3 = "BES", CountryName = "Bonaire, Sint Eustatius and Saba" });
            _alpha3Map.Add("BA", new CountryCodeEntry() { Alpha2 = "BA", Alpha3 = "BIH", CountryName = "Bosnia and Herzegovina" });
            _alpha3Map.Add("BW", new CountryCodeEntry() { Alpha2 = "BW", Alpha3 = "BWA", CountryName = "Botswana" });
            _alpha3Map.Add("BV", new CountryCodeEntry() { Alpha2 = "BV", Alpha3 = "BVT", CountryName = "Bouvet Island" });
            _alpha3Map.Add("BR", new CountryCodeEntry() { Alpha2 = "BR", Alpha3 = "BRA", CountryName = "Brazil" });
            _alpha3Map.Add("IO", new CountryCodeEntry() { Alpha2 = "IO", Alpha3 = "IOT", CountryName = "British Indian Ocean Territory" });
            _alpha3Map.Add("VG", new CountryCodeEntry() { Alpha2 = "VG", Alpha3 = "VGB", CountryName = "Virgin Islands (British)" });
            _alpha3Map.Add("BN", new CountryCodeEntry() { Alpha2 = "BN", Alpha3 = "BRN", CountryName = "Brunei Darussalam" });
            _alpha3Map.Add("BG", new CountryCodeEntry() { Alpha2 = "BG", Alpha3 = "BGR", CountryName = "Bulgaria" });
            _alpha3Map.Add("BF", new CountryCodeEntry() { Alpha2 = "BF", Alpha3 = "BFA", CountryName = "Burkina Faso" });
            _alpha3Map.Add("BI", new CountryCodeEntry() { Alpha2 = "BI", Alpha3 = "BDI", CountryName = "Burundi" });
            _alpha3Map.Add("CK", new CountryCodeEntry() { Alpha2 = "CK", Alpha3 = "COK", CountryName = "Cook Islands" });
            _alpha3Map.Add("CW", new CountryCodeEntry() { Alpha2 = "CW", Alpha3 = "CUW", CountryName = "Curaçao" });
            _alpha3Map.Add("TD", new CountryCodeEntry() { Alpha2 = "TD", Alpha3 = "TCD", CountryName = "Chad" });
            _alpha3Map.Add("ME", new CountryCodeEntry() { Alpha2 = "ME", Alpha3 = "MNE", CountryName = "Montenegro" });
            _alpha3Map.Add("CZ", new CountryCodeEntry() { Alpha2 = "CZ", Alpha3 = "CZE", CountryName = "Czech Republic" });
            _alpha3Map.Add("CN", new CountryCodeEntry() { Alpha2 = "CN", Alpha3 = "CHN", CountryName = "China" });
            _alpha3Map.Add("DK", new CountryCodeEntry() { Alpha2 = "DK", Alpha3 = "DNK", CountryName = "Denmark" });
            _alpha3Map.Add("CD", new CountryCodeEntry() { Alpha2 = "CD", Alpha3 = "COD", CountryName = "Congo (the Democratic Republic of the)" });
            _alpha3Map.Add("DM", new CountryCodeEntry() { Alpha2 = "DM", Alpha3 = "DMA", CountryName = "Dominica" });
            _alpha3Map.Add("DO", new CountryCodeEntry() { Alpha2 = "DO", Alpha3 = "DOM", CountryName = "Dominican Republic (the)" });
            _alpha3Map.Add("DJ", new CountryCodeEntry() { Alpha2 = "DJ", Alpha3 = "DJI", CountryName = "Djibouti" });
            _alpha3Map.Add("EG", new CountryCodeEntry() { Alpha2 = "EG", Alpha3 = "EGY", CountryName = "Egypt" });
            _alpha3Map.Add("EC", new CountryCodeEntry() { Alpha2 = "EC", Alpha3 = "ECU", CountryName = "Ecuador" });
            _alpha3Map.Add("ER", new CountryCodeEntry() { Alpha2 = "ER", Alpha3 = "ERI", CountryName = "Eritrea" });
            _alpha3Map.Add("EE", new CountryCodeEntry() { Alpha2 = "EE", Alpha3 = "EST", CountryName = "Estonia" });
            _alpha3Map.Add("ET", new CountryCodeEntry() { Alpha2 = "ET", Alpha3 = "ETH", CountryName = "Ethiopia" });
            _alpha3Map.Add("FO", new CountryCodeEntry() { Alpha2 = "FO", Alpha3 = "FRO", CountryName = "Faroe Islands" });
            _alpha3Map.Add("FK", new CountryCodeEntry() { Alpha2 = "FK", Alpha3 = "FLK", CountryName = "Falkland Islands (the) (Malvinas)" });
            _alpha3Map.Add("FJ", new CountryCodeEntry() { Alpha2 = "FJ", Alpha3 = "FJI", CountryName = "Fiji" });
            _alpha3Map.Add("PH", new CountryCodeEntry() { Alpha2 = "PH", Alpha3 = "PHL", CountryName = "Philippines" });
            _alpha3Map.Add("FI", new CountryCodeEntry() { Alpha2 = "FI", Alpha3 = "FIN", CountryName = "Finland" });
            _alpha3Map.Add("FR", new CountryCodeEntry() { Alpha2 = "FR", Alpha3 = "FRA", CountryName = "France" });
            _alpha3Map.Add("GF", new CountryCodeEntry() { Alpha2 = "GF", Alpha3 = "GUF", CountryName = "French Guiana" });
            _alpha3Map.Add("TF", new CountryCodeEntry() { Alpha2 = "TF", Alpha3 = "ATF", CountryName = "French Southern Territories" });
            _alpha3Map.Add("PF", new CountryCodeEntry() { Alpha2 = "PF", Alpha3 = "PYF", CountryName = "French Polynesia" });
            _alpha3Map.Add("GA", new CountryCodeEntry() { Alpha2 = "GA", Alpha3 = "GAB", CountryName = "Gabon" });
            _alpha3Map.Add("GM", new CountryCodeEntry() { Alpha2 = "GM", Alpha3 = "GMB", CountryName = "Gambia" });
            _alpha3Map.Add("GH", new CountryCodeEntry() { Alpha2 = "GH", Alpha3 = "GHA", CountryName = "Ghana" });
            _alpha3Map.Add("GI", new CountryCodeEntry() { Alpha2 = "GI", Alpha3 = "GIB", CountryName = "Gibraltar" });
            _alpha3Map.Add("GD", new CountryCodeEntry() { Alpha2 = "GD", Alpha3 = "GRD", CountryName = "Grenada" });
            _alpha3Map.Add("GL", new CountryCodeEntry() { Alpha2 = "GL", Alpha3 = "GRL", CountryName = "Greenland" });
            _alpha3Map.Add("GE", new CountryCodeEntry() { Alpha2 = "GE", Alpha3 = "GEO", CountryName = "Georgia" });
            _alpha3Map.Add("GP", new CountryCodeEntry() { Alpha2 = "GP", Alpha3 = "GLP", CountryName = "Guadeloupe" });
            _alpha3Map.Add("GU", new CountryCodeEntry() { Alpha2 = "GU", Alpha3 = "GUM", CountryName = "Guam" });
            _alpha3Map.Add("GT", new CountryCodeEntry() { Alpha2 = "GT", Alpha3 = "GTM", CountryName = "Guatemala" });
            _alpha3Map.Add("GG", new CountryCodeEntry() { Alpha2 = "GG", Alpha3 = "GGY", CountryName = "Guernsey" });
            _alpha3Map.Add("GN", new CountryCodeEntry() { Alpha2 = "GN", Alpha3 = "GIN", CountryName = "Guinea" });
            _alpha3Map.Add("GW", new CountryCodeEntry() { Alpha2 = "GW", Alpha3 = "GNB", CountryName = "Guinea-Bissau" });
            _alpha3Map.Add("GY", new CountryCodeEntry() { Alpha2 = "GY", Alpha3 = "GUY", CountryName = "Guyana" });
            _alpha3Map.Add("HT", new CountryCodeEntry() { Alpha2 = "HT", Alpha3 = "HTI", CountryName = "Haiti" });
            _alpha3Map.Add("HM", new CountryCodeEntry() { Alpha2 = "HM", Alpha3 = "HMD", CountryName = "Heard Island and McDonald Islands" });
            _alpha3Map.Add("HN", new CountryCodeEntry() { Alpha2 = "HN", Alpha3 = "HND", CountryName = "Honduras" });
            _alpha3Map.Add("HK", new CountryCodeEntry() { Alpha2 = "HK", Alpha3 = "HKG", CountryName = "Hong Kong" });
            _alpha3Map.Add("CL", new CountryCodeEntry() { Alpha2 = "CL", Alpha3 = "CHL", CountryName = "Chile" });
            _alpha3Map.Add("HR", new CountryCodeEntry() { Alpha2 = "HR", Alpha3 = "HRV", CountryName = "Croatia" });
            _alpha3Map.Add("IN", new CountryCodeEntry() { Alpha2 = "IN", Alpha3 = "IND", CountryName = "India" });
            _alpha3Map.Add("ID", new CountryCodeEntry() { Alpha2 = "ID", Alpha3 = "IDN", CountryName = "Indonesia" });
            _alpha3Map.Add("IQ", new CountryCodeEntry() { Alpha2 = "IQ", Alpha3 = "IRQ", CountryName = "Iraq" });
            _alpha3Map.Add("IR", new CountryCodeEntry() { Alpha2 = "IR", Alpha3 = "IRN", CountryName = "Iran (the Islamic Republic of)" });
            _alpha3Map.Add("IE", new CountryCodeEntry() { Alpha2 = "IE", Alpha3 = "IRL", CountryName = "Ireland" });
            _alpha3Map.Add("IS", new CountryCodeEntry() { Alpha2 = "IS", Alpha3 = "ISL", CountryName = "Iceland" });
            _alpha3Map.Add("IT", new CountryCodeEntry() { Alpha2 = "IT", Alpha3 = "ITA", CountryName = "Italy" });
            _alpha3Map.Add("IL", new CountryCodeEntry() { Alpha2 = "IL", Alpha3 = "ISR", CountryName = "Israel" });
            _alpha3Map.Add("JM", new CountryCodeEntry() { Alpha2 = "JM", Alpha3 = "JAM", CountryName = "Jamaica" });
            _alpha3Map.Add("JP", new CountryCodeEntry() { Alpha2 = "JP", Alpha3 = "JPN", CountryName = "Japan" });
            _alpha3Map.Add("YE", new CountryCodeEntry() { Alpha2 = "YE", Alpha3 = "YEM", CountryName = "Yemen" });
            _alpha3Map.Add("JE", new CountryCodeEntry() { Alpha2 = "JE", Alpha3 = "JEY", CountryName = "Jersey" });
            _alpha3Map.Add("ZA", new CountryCodeEntry() { Alpha2 = "ZA", Alpha3 = "ZAF", CountryName = "South Africa" });
            _alpha3Map.Add("GS", new CountryCodeEntry() { Alpha2 = "GS", Alpha3 = "SGS", CountryName = "South Georgia and the South Sandwich Islands" });
            _alpha3Map.Add("SS", new CountryCodeEntry() { Alpha2 = "SS", Alpha3 = "SSD", CountryName = "South Sudan" });
            _alpha3Map.Add("JO", new CountryCodeEntry() { Alpha2 = "JO", Alpha3 = "JOR", CountryName = "Jordan" });
            _alpha3Map.Add("KY", new CountryCodeEntry() { Alpha2 = "KY", Alpha3 = "CYM", CountryName = "Cayman Islands" });
            _alpha3Map.Add("KH", new CountryCodeEntry() { Alpha2 = "KH", Alpha3 = "KHM", CountryName = "Cambodia" });
            _alpha3Map.Add("CM", new CountryCodeEntry() { Alpha2 = "CM", Alpha3 = "CMR", CountryName = "Cameroon" });
            _alpha3Map.Add("CA", new CountryCodeEntry() { Alpha2 = "CA", Alpha3 = "CAN", CountryName = "Canada" });
            _alpha3Map.Add("CV", new CountryCodeEntry() { Alpha2 = "CV", Alpha3 = "CPV", CountryName = "Cape Verde" });
            _alpha3Map.Add("QA", new CountryCodeEntry() { Alpha2 = "QA", Alpha3 = "QAT", CountryName = "Qatar" });
            _alpha3Map.Add("KZ", new CountryCodeEntry() { Alpha2 = "KZ", Alpha3 = "KAZ", CountryName = "Kazakhstan" });
            _alpha3Map.Add("KE", new CountryCodeEntry() { Alpha2 = "KE", Alpha3 = "KEN", CountryName = "Kenya" });
            _alpha3Map.Add("KI", new CountryCodeEntry() { Alpha2 = "KI", Alpha3 = "KIR", CountryName = "Kiribati" });
            _alpha3Map.Add("CC", new CountryCodeEntry() { Alpha2 = "CC", Alpha3 = "CCK", CountryName = "Cocos (Keeling) Islands (the)" });
            _alpha3Map.Add("CO", new CountryCodeEntry() { Alpha2 = "CO", Alpha3 = "COL", CountryName = "Colombia" });
            _alpha3Map.Add("KM", new CountryCodeEntry() { Alpha2 = "KM", Alpha3 = "COM", CountryName = "Comoros" });
            _alpha3Map.Add("CG", new CountryCodeEntry() { Alpha2 = "CG", Alpha3 = "COG", CountryName = "Congo" });
            _alpha3Map.Add("KP", new CountryCodeEntry() { Alpha2 = "KP", Alpha3 = "PRK", CountryName = "Korea (the Democratic People's Republic of)" });
            _alpha3Map.Add("KR", new CountryCodeEntry() { Alpha2 = "KR", Alpha3 = "KOR", CountryName = "Korea (the Republic of)" });
            _alpha3Map.Add("XK", new CountryCodeEntry() { Alpha2 = "XK", Alpha3 = "XXK", CountryName = "Kosovo" });
            _alpha3Map.Add("CR", new CountryCodeEntry() { Alpha2 = "CR", Alpha3 = "CRI", CountryName = "Costa Rica" });
            _alpha3Map.Add("CU", new CountryCodeEntry() { Alpha2 = "CU", Alpha3 = "CUB", CountryName = "Cuba" });
            _alpha3Map.Add("KW", new CountryCodeEntry() { Alpha2 = "KW", Alpha3 = "KWT", CountryName = "Kuwait" });
            _alpha3Map.Add("CY", new CountryCodeEntry() { Alpha2 = "CY", Alpha3 = "CYP", CountryName = "Cyprus" });
            _alpha3Map.Add("KG", new CountryCodeEntry() { Alpha2 = "KG", Alpha3 = "KGZ", CountryName = "Kyrgyzstan" });
            _alpha3Map.Add("LA", new CountryCodeEntry() { Alpha2 = "LA", Alpha3 = "LAO", CountryName = "Lao People's Democratic Republic (the)" });
            _alpha3Map.Add("LS", new CountryCodeEntry() { Alpha2 = "LS", Alpha3 = "LSO", CountryName = "Lesotho" });
            _alpha3Map.Add("LB", new CountryCodeEntry() { Alpha2 = "LB", Alpha3 = "LBN", CountryName = "Lebanon" });
            _alpha3Map.Add("LR", new CountryCodeEntry() { Alpha2 = "LR", Alpha3 = "LBR", CountryName = "Liberia" });
            _alpha3Map.Add("LY", new CountryCodeEntry() { Alpha2 = "LY", Alpha3 = "LBY", CountryName = "Libya" });
            _alpha3Map.Add("LI", new CountryCodeEntry() { Alpha2 = "LI", Alpha3 = "LIE", CountryName = "Liechtenstein" });
            _alpha3Map.Add("LT", new CountryCodeEntry() { Alpha2 = "LT", Alpha3 = "LTU", CountryName = "Lithuania" });
            _alpha3Map.Add("LV", new CountryCodeEntry() { Alpha2 = "LV", Alpha3 = "LVA", CountryName = "Latvia" });
            _alpha3Map.Add("LU", new CountryCodeEntry() { Alpha2 = "LU", Alpha3 = "LUX", CountryName = "Luxembourg" });
            _alpha3Map.Add("MO", new CountryCodeEntry() { Alpha2 = "MO", Alpha3 = "MAC", CountryName = "Macao" });
            _alpha3Map.Add("MG", new CountryCodeEntry() { Alpha2 = "MG", Alpha3 = "MDG", CountryName = "Madagascar" });
            _alpha3Map.Add("HU", new CountryCodeEntry() { Alpha2 = "HU", Alpha3 = "HUN", CountryName = "Hungary" });
            _alpha3Map.Add("MK", new CountryCodeEntry() { Alpha2 = "MK", Alpha3 = "MKD", CountryName = "Macedonia (the former Yugoslav Republic of)" });
            _alpha3Map.Add("MY", new CountryCodeEntry() { Alpha2 = "MY", Alpha3 = "MYS", CountryName = "Malaysia" });
            _alpha3Map.Add("MW", new CountryCodeEntry() { Alpha2 = "MW", Alpha3 = "MWI", CountryName = "Malawi" });
            _alpha3Map.Add("MV", new CountryCodeEntry() { Alpha2 = "MV", Alpha3 = "MDV", CountryName = "Maldives" });
            _alpha3Map.Add("ML", new CountryCodeEntry() { Alpha2 = "ML", Alpha3 = "MLI", CountryName = "Mali" });
            _alpha3Map.Add("MT", new CountryCodeEntry() { Alpha2 = "MT", Alpha3 = "MLT", CountryName = "Malta" });
            _alpha3Map.Add("IM", new CountryCodeEntry() { Alpha2 = "IM", Alpha3 = "IMN", CountryName = "Isle of Man" });
            _alpha3Map.Add("MA", new CountryCodeEntry() { Alpha2 = "MA", Alpha3 = "MAR", CountryName = "Morocco" });
            _alpha3Map.Add("MH", new CountryCodeEntry() { Alpha2 = "MH", Alpha3 = "MHL", CountryName = "Marshall Islands (the)" });
            _alpha3Map.Add("MQ", new CountryCodeEntry() { Alpha2 = "MQ", Alpha3 = "MTQ", CountryName = "Martinique" });
            _alpha3Map.Add("MU", new CountryCodeEntry() { Alpha2 = "MU", Alpha3 = "MUS", CountryName = "Mauritius" });
            _alpha3Map.Add("MR", new CountryCodeEntry() { Alpha2 = "MR", Alpha3 = "MRT", CountryName = "Mauritania" });
            _alpha3Map.Add("YT", new CountryCodeEntry() { Alpha2 = "YT", Alpha3 = "MYT", CountryName = "Mayotte" });
            _alpha3Map.Add("UM", new CountryCodeEntry() { Alpha2 = "UM", Alpha3 = "UMI", CountryName = "United States Minor Outlying Islands (the)" });
            _alpha3Map.Add("MX", new CountryCodeEntry() { Alpha2 = "MX", Alpha3 = "MEX", CountryName = "Mexico" });
            _alpha3Map.Add("FM", new CountryCodeEntry() { Alpha2 = "FM", Alpha3 = "FSM", CountryName = "Micronesia (the Federated States of)" });
            _alpha3Map.Add("MD", new CountryCodeEntry() { Alpha2 = "MD", Alpha3 = "MDA", CountryName = "Moldova (the Republic of)" });
            _alpha3Map.Add("MC", new CountryCodeEntry() { Alpha2 = "MC", Alpha3 = "MCO", CountryName = "Monaco" });
            _alpha3Map.Add("MN", new CountryCodeEntry() { Alpha2 = "MN", Alpha3 = "MNG", CountryName = "Mongolia" });
            _alpha3Map.Add("MS", new CountryCodeEntry() { Alpha2 = "MS", Alpha3 = "MSR", CountryName = "Montserrat" });
            _alpha3Map.Add("MZ", new CountryCodeEntry() { Alpha2 = "MZ", Alpha3 = "MOZ", CountryName = "Mozambique" });
            _alpha3Map.Add("MM", new CountryCodeEntry() { Alpha2 = "MM", Alpha3 = "MMR", CountryName = "Myanmar" });
            _alpha3Map.Add("NA", new CountryCodeEntry() { Alpha2 = "NA", Alpha3 = "NAM", CountryName = "Namibia" });
            _alpha3Map.Add("NR", new CountryCodeEntry() { Alpha2 = "NR", Alpha3 = "NRU", CountryName = "Nauru" });
            _alpha3Map.Add("DE", new CountryCodeEntry() { Alpha2 = "DE", Alpha3 = "DEU", CountryName = "Germany" });
            _alpha3Map.Add("NP", new CountryCodeEntry() { Alpha2 = "NP", Alpha3 = "NPL", CountryName = "Nepal" });
            _alpha3Map.Add("NE", new CountryCodeEntry() { Alpha2 = "NE", Alpha3 = "NER", CountryName = "Niger (the)" });
            _alpha3Map.Add("NG", new CountryCodeEntry() { Alpha2 = "NG", Alpha3 = "NGA", CountryName = "Nigeria" });
            _alpha3Map.Add("NI", new CountryCodeEntry() { Alpha2 = "NI", Alpha3 = "NIC", CountryName = "Nicaragua" });
            _alpha3Map.Add("NU", new CountryCodeEntry() { Alpha2 = "NU", Alpha3 = "NIU", CountryName = "Niue" });
            _alpha3Map.Add("NL", new CountryCodeEntry() { Alpha2 = "NL", Alpha3 = "NLD", CountryName = "Netherlands (the)" });
            _alpha3Map.Add("NF", new CountryCodeEntry() { Alpha2 = "NF", Alpha3 = "NFK", CountryName = "Norfolk Island" });
            _alpha3Map.Add("NO", new CountryCodeEntry() { Alpha2 = "NO", Alpha3 = "NOR", CountryName = "Norway" });
            _alpha3Map.Add("NC", new CountryCodeEntry() { Alpha2 = "NC", Alpha3 = "NCL", CountryName = "New Caledonia" });
            _alpha3Map.Add("NZ", new CountryCodeEntry() { Alpha2 = "NZ", Alpha3 = "NZL", CountryName = "New Zealand" });
            _alpha3Map.Add("OM", new CountryCodeEntry() { Alpha2 = "OM", Alpha3 = "OMN", CountryName = "Oman" });
            _alpha3Map.Add("PK", new CountryCodeEntry() { Alpha2 = "PK", Alpha3 = "PAK", CountryName = "Pakistan" });
            _alpha3Map.Add("PW", new CountryCodeEntry() { Alpha2 = "PW", Alpha3 = "PLW", CountryName = "Palau" });
            _alpha3Map.Add("PS", new CountryCodeEntry() { Alpha2 = "PS", Alpha3 = "PSE", CountryName = "Palestine, State of" });
            _alpha3Map.Add("PA", new CountryCodeEntry() { Alpha2 = "PA", Alpha3 = "PAN", CountryName = "Panama" });
            _alpha3Map.Add("PG", new CountryCodeEntry() { Alpha2 = "PG", Alpha3 = "PNG", CountryName = "Papua New Guinea" });
            _alpha3Map.Add("PY", new CountryCodeEntry() { Alpha2 = "PY", Alpha3 = "PRY", CountryName = "Paraguay" });
            _alpha3Map.Add("PE", new CountryCodeEntry() { Alpha2 = "PE", Alpha3 = "PER", CountryName = "Peru" });
            _alpha3Map.Add("PN", new CountryCodeEntry() { Alpha2 = "PN", Alpha3 = "PCN", CountryName = "Pitcairn" });
            _alpha3Map.Add("CI", new CountryCodeEntry() { Alpha2 = "CI", Alpha3 = "CIV", CountryName = "Côte d'Ivoire" });
            _alpha3Map.Add("PL", new CountryCodeEntry() { Alpha2 = "PL", Alpha3 = "POL", CountryName = "Poland" });
            _alpha3Map.Add("PR", new CountryCodeEntry() { Alpha2 = "PR", Alpha3 = "PRI", CountryName = "Puerto Rico" });
            _alpha3Map.Add("PT", new CountryCodeEntry() { Alpha2 = "PT", Alpha3 = "PRT", CountryName = "Portugal" });
            _alpha3Map.Add("AT", new CountryCodeEntry() { Alpha2 = "AT", Alpha3 = "AUT", CountryName = "Austria" });
            _alpha3Map.Add("RE", new CountryCodeEntry() { Alpha2 = "RE", Alpha3 = "REU", CountryName = "Réunion" });
            _alpha3Map.Add("GQ", new CountryCodeEntry() { Alpha2 = "GQ", Alpha3 = "GNQ", CountryName = "Equatorial Guinea" });
            _alpha3Map.Add("RO", new CountryCodeEntry() { Alpha2 = "RO", Alpha3 = "ROU", CountryName = "Romania" });
            _alpha3Map.Add("RU", new CountryCodeEntry() { Alpha2 = "RU", Alpha3 = "RUS", CountryName = "Russian Federation (the)" });
            _alpha3Map.Add("RW", new CountryCodeEntry() { Alpha2 = "RW", Alpha3 = "RWA", CountryName = "Rwanda" });
            _alpha3Map.Add("GR", new CountryCodeEntry() { Alpha2 = "GR", Alpha3 = "GRC", CountryName = "Greece" });
            _alpha3Map.Add("PM", new CountryCodeEntry() { Alpha2 = "PM", Alpha3 = "SPM", CountryName = "Saint Pierre and Miquelon" });
            _alpha3Map.Add("SV", new CountryCodeEntry() { Alpha2 = "SV", Alpha3 = "SLV", CountryName = "El Salvador" });
            _alpha3Map.Add("WS", new CountryCodeEntry() { Alpha2 = "WS", Alpha3 = "WSM", CountryName = "Samoa" });
            _alpha3Map.Add("SM", new CountryCodeEntry() { Alpha2 = "SM", Alpha3 = "SMR", CountryName = "San Marino" });
            _alpha3Map.Add("SA", new CountryCodeEntry() { Alpha2 = "SA", Alpha3 = "SAU", CountryName = "Saudi Arabia" });
            _alpha3Map.Add("SN", new CountryCodeEntry() { Alpha2 = "SN", Alpha3 = "SEN", CountryName = "Senegal" });
            _alpha3Map.Add("MP", new CountryCodeEntry() { Alpha2 = "MP", Alpha3 = "MNP", CountryName = "Northern Mariana Islands (the)" });
            _alpha3Map.Add("SC", new CountryCodeEntry() { Alpha2 = "SC", Alpha3 = "SYC", CountryName = "Seychelles" });
            _alpha3Map.Add("SL", new CountryCodeEntry() { Alpha2 = "SL", Alpha3 = "SLE", CountryName = "Sierra Leone" });
            _alpha3Map.Add("SG", new CountryCodeEntry() { Alpha2 = "SG", Alpha3 = "SGP", CountryName = "Singapore" });
            _alpha3Map.Add("SK", new CountryCodeEntry() { Alpha2 = "SK", Alpha3 = "SVK", CountryName = "Slovakia" });
            _alpha3Map.Add("SI", new CountryCodeEntry() { Alpha2 = "SI", Alpha3 = "SVN", CountryName = "Slovenia" });
            _alpha3Map.Add("SO", new CountryCodeEntry() { Alpha2 = "SO", Alpha3 = "SOM", CountryName = "Somalia" });
            _alpha3Map.Add("AE", new CountryCodeEntry() { Alpha2 = "AE", Alpha3 = "ARE", CountryName = "United Arab Emirates (the)" });
            _alpha3Map.Add("US", new CountryCodeEntry() { Alpha2 = "US", Alpha3 = "USA", CountryName = "United States (the)" });
            _alpha3Map.Add("RS", new CountryCodeEntry() { Alpha2 = "RS", Alpha3 = "SRB", CountryName = "Serbia" });
            _alpha3Map.Add("CF", new CountryCodeEntry() { Alpha2 = "CF", Alpha3 = "CAF", CountryName = "Central African Republic (the)" });
            _alpha3Map.Add("SD", new CountryCodeEntry() { Alpha2 = "SD", Alpha3 = "SDN", CountryName = "Sudan (the)" });
            _alpha3Map.Add("SR", new CountryCodeEntry() { Alpha2 = "SR", Alpha3 = "SUR", CountryName = "Suriname" });
            _alpha3Map.Add("SH", new CountryCodeEntry() { Alpha2 = "SH", Alpha3 = "SHN", CountryName = "Saint Helena, Ascension and Tristan da Cunha" });
            _alpha3Map.Add("LC", new CountryCodeEntry() { Alpha2 = "LC", Alpha3 = "LCA", CountryName = "Saint Lucia" });
            _alpha3Map.Add("BL", new CountryCodeEntry() { Alpha2 = "BL", Alpha3 = "BLM", CountryName = "Saint Barthélemy" });
            _alpha3Map.Add("KN", new CountryCodeEntry() { Alpha2 = "KN", Alpha3 = "KNA", CountryName = "Saint Kitts and Nevis" });
            _alpha3Map.Add("MF", new CountryCodeEntry() { Alpha2 = "MF", Alpha3 = "MAF", CountryName = "Saint Martin (French part)" });
            _alpha3Map.Add("SX", new CountryCodeEntry() { Alpha2 = "SX", Alpha3 = "SXM", CountryName = "Sint Maarten (Dutch part)" });
            _alpha3Map.Add("ST", new CountryCodeEntry() { Alpha2 = "ST", Alpha3 = "STP", CountryName = "Sao Tome and Principe" });
            _alpha3Map.Add("VC", new CountryCodeEntry() { Alpha2 = "VC", Alpha3 = "VCT", CountryName = "Saint Vincent and the Grenadines" });
            _alpha3Map.Add("SZ", new CountryCodeEntry() { Alpha2 = "SZ", Alpha3 = "SWZ", CountryName = "Swaziland" });
            _alpha3Map.Add("SY", new CountryCodeEntry() { Alpha2 = "SY", Alpha3 = "SYR", CountryName = "Syrian Arab Republic (the)" });
            _alpha3Map.Add("SB", new CountryCodeEntry() { Alpha2 = "SB", Alpha3 = "SLB", CountryName = "Solomon Islands (the)" });
            _alpha3Map.Add("ES", new CountryCodeEntry() { Alpha2 = "ES", Alpha3 = "ESP", CountryName = "Spain" });
            _alpha3Map.Add("SJ", new CountryCodeEntry() { Alpha2 = "SJ", Alpha3 = "SJM", CountryName = "Svalbard and Jan Mayen" });
            _alpha3Map.Add("LK", new CountryCodeEntry() { Alpha2 = "LK", Alpha3 = "LKA", CountryName = "Sri Lanka" });
            _alpha3Map.Add("SE", new CountryCodeEntry() { Alpha2 = "SE", Alpha3 = "SWE", CountryName = "Sweden" });
            _alpha3Map.Add("CH", new CountryCodeEntry() { Alpha2 = "CH", Alpha3 = "CHE", CountryName = "Switzerland" });
            _alpha3Map.Add("TJ", new CountryCodeEntry() { Alpha2 = "TJ", Alpha3 = "TJK", CountryName = "Tajikistan" });
            _alpha3Map.Add("TZ", new CountryCodeEntry() { Alpha2 = "TZ", Alpha3 = "TZA", CountryName = "Tanzania, United Republic of" });
            _alpha3Map.Add("TH", new CountryCodeEntry() { Alpha2 = "TH", Alpha3 = "THA", CountryName = "Thailand" });
            _alpha3Map.Add("TW", new CountryCodeEntry() { Alpha2 = "TW", Alpha3 = "TWN", CountryName = "Taiwan (Province of China)" });
            _alpha3Map.Add("TG", new CountryCodeEntry() { Alpha2 = "TG", Alpha3 = "TGO", CountryName = "Togo" });
            _alpha3Map.Add("TK", new CountryCodeEntry() { Alpha2 = "TK", Alpha3 = "TKL", CountryName = "Tokelau" });
            _alpha3Map.Add("TO", new CountryCodeEntry() { Alpha2 = "TO", Alpha3 = "TON", CountryName = "Tonga" });
            _alpha3Map.Add("TT", new CountryCodeEntry() { Alpha2 = "TT", Alpha3 = "TTO", CountryName = "Trinidad and Tobago" });
            _alpha3Map.Add("TN", new CountryCodeEntry() { Alpha2 = "TN", Alpha3 = "TUN", CountryName = "Tunisia" });
            _alpha3Map.Add("TR", new CountryCodeEntry() { Alpha2 = "TR", Alpha3 = "TUR", CountryName = "Turkey" });
            _alpha3Map.Add("TM", new CountryCodeEntry() { Alpha2 = "TM", Alpha3 = "TKM", CountryName = "Turkmenistan" });
            _alpha3Map.Add("TC", new CountryCodeEntry() { Alpha2 = "TC", Alpha3 = "TCA", CountryName = "Turks and Caicos Islands (the)" });
            _alpha3Map.Add("TV", new CountryCodeEntry() { Alpha2 = "TV", Alpha3 = "TUV", CountryName = "Tuvalu" });
            _alpha3Map.Add("UG", new CountryCodeEntry() { Alpha2 = "UG", Alpha3 = "UGA", CountryName = "Uganda" });
            _alpha3Map.Add("UA", new CountryCodeEntry() { Alpha2 = "UA", Alpha3 = "UKR", CountryName = "Ukraine" });
            _alpha3Map.Add("UY", new CountryCodeEntry() { Alpha2 = "UY", Alpha3 = "URY", CountryName = "Uruguay" });
            _alpha3Map.Add("UZ", new CountryCodeEntry() { Alpha2 = "UZ", Alpha3 = "UZB", CountryName = "Uzbekistan" });
            _alpha3Map.Add("CX", new CountryCodeEntry() { Alpha2 = "CX", Alpha3 = "CXR", CountryName = "Christmas Island" });
            _alpha3Map.Add("VU", new CountryCodeEntry() { Alpha2 = "VU", Alpha3 = "VUT", CountryName = "Vanuatu" });
            _alpha3Map.Add("VA", new CountryCodeEntry() { Alpha2 = "VA", Alpha3 = "VAT", CountryName = "Holy See (the) (Vatican City State)" });
            _alpha3Map.Add("GB", new CountryCodeEntry() { Alpha2 = "GB", Alpha3 = "GBR", CountryName = "United Kingdom (the)" });
            _alpha3Map.Add("VE", new CountryCodeEntry() { Alpha2 = "VE", Alpha3 = "VEN", CountryName = "Venezuela, Bolivarian Republic of" });
            _alpha3Map.Add("VN", new CountryCodeEntry() { Alpha2 = "VN", Alpha3 = "VNM", CountryName = "Viet Nam" });
            _alpha3Map.Add("TL", new CountryCodeEntry() { Alpha2 = "TL", Alpha3 = "TLS", CountryName = "Timor-Leste" });
            _alpha3Map.Add("WF", new CountryCodeEntry() { Alpha2 = "WF", Alpha3 = "WLF", CountryName = "Wallis and Futuna" });
            _alpha3Map.Add("ZM", new CountryCodeEntry() { Alpha2 = "ZM", Alpha3 = "ZMB", CountryName = "Zambia" });
            _alpha3Map.Add("EH", new CountryCodeEntry() { Alpha2 = "EH", Alpha3 = "ESH", CountryName = "Western Sahara" });
            _alpha3Map.Add("ZW", new CountryCodeEntry() { Alpha2 = "ZW", Alpha3 = "ZWE", CountryName = "Zimbabwe" });
        }
    }

    /// <summary>
    /// CountryCode map item
    /// </summary>
    public class CountryCodeEntry
    {
        /// <summary>
        /// 2-letters code for country
        /// ISO 3166-1 Alpha2
        /// </summary>
        public string Alpha2 { get; set; }

        /// <summary>
        /// 3-letters code for country
        /// ISO 3166-1 Alpha3
        /// </summary>
        public string Alpha3 { get; set; }

        /// <summary>       
        /// English abbreviation of country name
        /// </summary>
        public string CountryName { get; set; }

        public override int GetHashCode() =>  Alpha2.GetHashCode() + Alpha3.GetHashCode() + CountryName.GetHashCode();        

        public override bool Equals(object obj)
        {
            if (obj is CountryCodeEntry)
            {
                CountryCodeEntry other = obj as CountryCodeEntry;
                return Alpha2.Equals(other.Alpha2) & Alpha3.Equals(other.Alpha3) & CountryName.Equals(other.CountryName);
            }

            return false;
        }
    }
}
