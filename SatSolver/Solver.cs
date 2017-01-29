using System;
using System.Collections.Generic;

namespace SatSolver
{
    public class Solver
    {
        /* 
		 * Deze methode geeft een Valuatie terug die de gegeven Formule vervult.
		 * Wanneer zo'n Valuatie niet bestaat geeft hij de waarde null terug.
		 * 
		 * Deze methode roept de gelijknamige recursieve methode met 3 parameters aan, met de volgende initiele waarden:
		 * - formule       de gegeven Formule,
		 * - variabelen    de Set van alle variabelen uit de Formule, verkregen door eerst de methode Verzamel aan te roepen,
		 * - valuatie      de lege valuatie.
		 */
        public static Valuatie Vervulbaar(IFormule formule)
        {
            if (formule == null)
                return null;

            SortedSet<string> variabelen = new SortedSet<string>();
            formule.Verzamel(variabelen);

            Valuatie valuatie = new Valuatie();
            return Vervulbaar(formule, variabelen, valuatie);
        }

        /* 
		 * Deze recursieve methode krijgt een Formule, een Set van nog te valueren variabelen,
		 * en een Valuatie voor een deel van de variabelen.
		 * 
		 * Deze methode geeft een Valuatie terug die de gegeven formule vervult.
		 * Wanneer zo'n Valuatie niet bestaat geeft hij de waarde null terug.
		 */
        private static Valuatie Vervulbaar(IFormule formule, SortedSet<string> variabelen, Valuatie valuatie)
        {
            Valuatie resultaat;
            string var = GetElement(variabelen);

            if (var == null) return valuatie;

            // Haal variabele uit lijst van te checken variabelen
            variabelen.Remove(var);

            // Controleer of formule vervulbaar is voor de waarde (eerst waar, want als zowel waar als onwaar mogelijk is, wordt waar gekozen)
            resultaat = VervulbaarVoorWaarde(formule, variabelen, valuatie, var, true);
            if (resultaat != null) return resultaat;
            resultaat = VervulbaarVoorWaarde(formule, variabelen, valuatie, var, false);
            if (resultaat != null) return resultaat;
            return null;
        }

        /* 
		 * Deze methode controleert de vervulbaarheid van een formule voor een bepaalde waarde voor een enkele variabele
		 */
        private static Valuatie VervulbaarVoorWaarde(IFormule formule, SortedSet<string> variabelen, Valuatie valuatie, string var, bool waarde)
        {
            // Voeg variabele toe aan valuatie
            valuatie.VoegToe(var, waarde);
            // Controleer of formule waar kan zijn
            if (formule.KanWaar(valuatie))
            {
                // Controleer recursief
                Valuatie resultaat = Vervulbaar(formule, variabelen, valuatie);
                // Als er een resultaat is, return deze
                if (resultaat != null) return resultaat;
            }
            // Anders verwijder de variabele weer uit de valuatie en return null
            valuatie.Verwijder(var);
            return null;
        }

        /* 
		 * Deze hulp-methode geeft het eerste element van een ISet terug.
		 */
        private static string GetElement(ISet<string> set)
        {
            foreach (string s in set)  // we prepareren ons om alle elementen te doorlopen...
                return s;              // ...maar we pakken meteen de eerste!
            return null; // in geval van een lege verzameling
        }
    }
}
