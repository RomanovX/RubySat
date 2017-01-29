using System.Collections.Generic;

namespace SatSolver
{
    public class Valuatie
    {
        private readonly SortedDictionary<string, bool> dictionary;

        // Constructor.
        public Valuatie()
        {
            dictionary = new SortedDictionary<string, bool>();
        }

        // Geeft terug of deze valuatie de gegeven variabele bevat.
        public bool BevatVariabele(string variabele)
        {
            return dictionary.ContainsKey(variabele);
        }

        // Geeft de waarde van de gegeven variabele terug.
        public bool GeefWaarde(string variabele)
        {
            return dictionary[variabele];
        }

        // Voegt de gegeven variabele met de gegeven waarde toe aan deze valuatie.
        public void VoegToe(string variabele, bool waarde)
        {
            dictionary.Add(variabele, waarde);
        }

        // Verwijdert de gegeven variabele uit deze valuatie.
        public void Verwijder(string variabele)
        {
            dictionary.Remove(variabele);
        }

        // Geeft een stringrepresentatie van deze valuatie terug.
        public override string ToString()
        {
            string resultaat = "";
            foreach (KeyValuePair<string, bool> pair in dictionary)
            {
                if (resultaat != "")
                    resultaat += " ";
                resultaat += pair.Key + "=" + pair.Value;
            }
            return resultaat;
        }
    }
}
