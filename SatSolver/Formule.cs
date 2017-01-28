using System.Collections.Generic;

namespace SatSolver
{
    /*
     *  Deze interface beschrijft wat er mogelijk moet zijn met een formule
     */
    public interface IFormule
    {
        void Verzamel(ISet<string> set);
        string ToString();
        bool Waarde(Valuatie v);
        bool KanWaar(Valuatie v);
        bool KanOnwaar(Valuatie v);

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
            return variabele;
        }
        public bool Waarde(Valuatie v)
        { 
            return v.GeefWaarde(variabele);
        }
        public bool KanWaar(Valuatie v)
        {
            // Een propositie kan waar zijn als er nog geen valuatie aan is toegekend, of de valuatie waar is
            if (!v.BevatVariabele(variabele)) return true;
            return v.GeefWaarde(variabele);
        }
        public bool KanOnwaar(Valuatie v)
        {
            // Een propositie kan onwaar zijn als er nog geen valuatie aan is toegekend, of de valuatie onwaar is
            if (!v.BevatVariabele(variabele)) return true;
            return !v.GeefWaarde(variabele);
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
            return "(" + links.ToString() + "/\\" + rechts.ToString() + ")";
        }
        public bool Waarde(Valuatie v)
        {
            return links.Waarde(v) && rechts.Waarde(v);
        }
        public bool KanWaar(Valuatie v)
        {
            // Een conjuctie kan enkel waar zijn als beide elementen waar kunnen zijn
            return links.KanWaar(v) && rechts.KanWaar(v);
        }
        public bool KanOnwaar(Valuatie v)
        {
            // Een conjuctie kan onwaar zijn als één of beide van de elementen onwaar kunnen zijn
            return links.KanOnwaar(v) || rechts.KanOnwaar(v);
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
            return ("(" + links.ToString() + "\\/" + rechts.ToString() + ")");
        }
        public bool Waarde(Valuatie v)
        {
            return links.Waarde(v) || rechts.Waarde(v);
        }
        public bool KanWaar(Valuatie v)
        {
            // Een disjunctie kan waar zijn als één of beide van de elementen waar kunnen zijn
            return links.KanWaar(v) || rechts.KanWaar(v);
        }
        public bool KanOnwaar(Valuatie v)
        {
            // Een disjunctie kan enkel onwaar zijn als beide elementen onwaar kunnen zijn
            return links.KanOnwaar(v) && rechts.KanOnwaar(v);
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
            return "-" + formule.ToString();
        }
        public bool Waarde(Valuatie v)
        {
            return !formule.Waarde(v);
        }
        public bool KanWaar(Valuatie v)
        {
            // Een negatie kan waar zijn als het element onwaar kan zijn
            return formule.KanOnwaar(v);
        }
        public bool KanOnwaar(Valuatie v)
        {
            // Een negatie kan onwaar zijn als 
            return formule.KanWaar(v);
        }
    }
}