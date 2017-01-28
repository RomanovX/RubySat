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
            // TODO: schrijf de methode die een Valuatie vindt die een formule vervult
            ////Roep de functie tweemaal recursief aan met een kleinere set variabelen en een iets grotere valuatie maar alleen als dat kansrijk is
            ////uit powerpoint:
            if (variabelen.Count == 0)
            {
                //return ?;
            }
            else
            {
                //return ?;
            }
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
