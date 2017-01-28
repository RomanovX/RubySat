using System.Collections.Generic;

namespace SatSolver /* Hoi Ruby */
{
    /*
     *  Deze interface beschrijft wat er mogelijk moet zijn met een formule
     */
    public interface IFormule
    {
        void Verzamel(ISet<string> set);
        string ToString();
        bool Waarde(Valuatie v);

        // TODO: Voeg aan de interface IFormule specificaties toe van overige methoden die nodig zijn in je methode Solver.Vervulbaar
    }

    class Variabele : IFormule
    {
        string variabele;
        public Variabele (string variabele)
        {
            this.variabele = variabele;
        }
        public void Verzamel (ISet<string> set)
        {
            set.Add(variabele);
        }
        public override string ToString()
        {
            return ("(" + variabele + ")");
        }
        public bool Waarde(Valuatie v)
        { 
            return v.GeefWaarde(variabele);
        }
    }

    class Conjunctie : IFormule
    {
        IFormule links;
        IFormule rechts;
        public Conjunctie (IFormule links, IFormule rechts)
        {
            this.links = links;
            this.rechts = rechts;
        }
        public void Verzamel (ISet<string> set)
        {
            links.Verzamel(set);
            rechts.Verzamel(set);
        }
        public override string ToString()
        {
            string formule = ("(" + links.ToString() + "/\\" + rechts.ToString() + ")");
            return formule;
        }
        public bool Waarde(Valuatie v)
        {
            return links.Waarde(v) && rechts.Waarde(v);
        }
    }

    class Disjunctie : IFormule
    {
        IFormule links;
        IFormule rechts;
        public Disjunctie(IFormule links, IFormule rechts)
        {
            this.links = links;
            this.rechts = rechts;
        }
        public void Verzamel(ISet<string> set)
        {
            links.Verzamel(set);
            rechts.Verzamel(set);
        }
        public override string ToString()
        {
            string formule = ("(" + links.ToString() + "\\/" + rechts.ToString() + ")");
            return formule;
        }
        public bool Waarde(Valuatie v)
        {
            return links.Waarde(v) && rechts.Waarde(v);
        }
    }

    class Negatie : IFormule
    {
        IFormule formule;
        public Negatie(IFormule formule)
        {
            this.formule = formule;
        }
        public void Verzamel (ISet<string>set)
        {
            formule.Verzamel(set);
        }
        public override string ToString()
        {
            string formule2 = ("(-" + formule.ToString() + ")");
            return formule2;
        }
        public bool Waarde(Valuatie v)
        {
            return !formule.Waarde(v);
        }
    }



    // TODO: Maak verschillende klassen die de interface IFormule implementeren, voor de diverse soorten proposities die er bestaan
}