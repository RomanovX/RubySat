using System;

namespace SatSolver
{
    class Parser
    {
        private string inhoud;
        private int cursor;
        private int lengte;

        /*
         * Dit is de enige public methode in deze klasse: de methode ontleedt een string tot een formule.
         * Als dat niet mogelijk is (omdat de string een syntactische fout bevat) werpt de methode een exception.
         */
        public static IFormule ParseFormule(string s)
        {
            Parser parser = new Parser(s);
            return parser.ParseFormule();
        }

        /*
		 * De constructor bewaart de string die ontleed moet worden, 
         * en initialiseert een "cursor" waarmee we kunnen aanwijzen tot hoe ver het ontleedproces gevorderd is.
		 */
        private Parser(string s)
        {
            inhoud = s;
            cursor = 0;
            lengte = s.Length;
        }

        /*
         * Deze hulpmethode zorgt ervoor dat de cursor eventuele extra spaties/tabs/newlines in de string passeert.
         */
        private void SkipSpaces()
        {
            while (cursor < lengte && char.IsWhiteSpace(inhoud[cursor]))
                cursor++;
        }

        /*
         * Deze methode start het recursieve ontleedproces op, 
         * en controleert na afloop of inderdaad de hele invoer is geconsumeerd.
         */
        private IFormule ParseFormule()
        {
            IFormule e = ParseExpressie();
            SkipSpaces();
            if (cursor < lengte)
            {
                throw new Exception("Extra input op positie" + cursor + "(" + inhoud[cursor] + ")");
            }
            return e;
        }

        /* 
         * Het eigenlijk werk wordt gedaan door drie wederzijds recursieve methodes: 
         * - ParseExpressie, die een complete propositie ontleedt
         * - ParseTerm, die een formule ontleedt waarin op top-nivo geen of-operatoren worden gebruikt
         * - ParseFactor, die alleen maar een losse variabele verwerkt, 
         *    of een negatie, of een complete propositie, die dan wel tussen haakjes moet staan.
         * De methodes leveren de herkende deelformule op, 
         * en verplaatsen de cursor naar de positie in de invoer daar net voorbij.
         */
        private IFormule ParseFactor()
        {
            SkipSpaces();

            if (cursor < lengte && inhoud[cursor] == '(')
            {
                cursor++; // passeer het openingshaakje
                IFormule resultaat = ParseExpressie(); // tussen de haakjes mag een complete propositie staan
                SkipSpaces();
                if (inhoud[cursor] != ')') throw new Exception("sluithaakje ontbreekt op positie " + cursor);
                cursor++; // passeer het sluithaakje
                return resultaat;
            }

            if (cursor < lengte && (inhoud[cursor] == '-' || inhoud[cursor] == '!' || inhoud[cursor] == '~'))
            {
                cursor += 1; // passeer het negatieteken
                IFormule resultaat = ParseFactor();
                return MaakNegatie(resultaat);
            }

            // geen haakje, geen not-teken, dus dan moeten we een variabele herkennen
            string variabele = "";

            // Controleer alle opvolgende letters en cijfers en maak er een variabele van
            while (cursor < lengte && Char.IsLetterOrDigit(inhoud[cursor]))
            {
                variabele += inhoud[cursor];
                cursor += 1;
            }
            return MaakPropositie(variabele);
        }

        private IFormule ParseTerm()
        {
            IFormule f = ParseFactor();
            SkipSpaces();
            if (cursor < lengte - 1 && (inhoud[cursor] == '/' && inhoud[cursor + 1] == '\\' || inhoud[cursor] == '&' && inhoud[cursor + 1] == '&'))
            {
                cursor += 2; // passeer het voegteken
                IFormule t = ParseTerm();
                return MaakConjunctie(f, t);
            }
            return f;
        }

        private IFormule ParseExpressie()
        {
            IFormule t = ParseTerm();
            SkipSpaces();
            if (cursor < lengte - 1 && (inhoud[cursor] == '\\' && inhoud[cursor + 1] == '/' || inhoud[cursor] == '|' && inhoud[cursor + 1] == '|'))
            {
                cursor += 2; // passeer het voegteken
                IFormule e = ParseExpressie();
                return MaakDisjunctie(t, e);
            }
            return t;
        }

        /*
         * Deze vier hulpmethoden maken een object aan voor de vier verschillende formule-vormen.
         */
        static IFormule MaakPropositie(string variabele)
        {
            return new Variabele(variabele);
        }

        static IFormule MaakNegatie(IFormule formule)
        {
            return new Negatie(formule);
        }

        static IFormule MaakConjunctie(IFormule links, IFormule rechts)
        {
            return new Conjunctie(links, rechts);
        }

        static IFormule MaakDisjunctie(IFormule links, IFormule rechts)
        {
            return new Disjunctie(links, rechts);
        }
    }
}
