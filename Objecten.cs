using System.Windows.Forms;
using System.Drawing;

namespace SchetsEditor
{
    public interface iTekenObject
    {
        void Teken(SchetsControl s);
        void verwijder(SchetsControl s);
        bool geselecteerd(SchetsControl s, Point p);

    }
    abstract public class TekenObject : iTekenObject

    {
        public Point startpunt, eindpunt;
        public Color kleur;

        public TekenObject(Point startpunt, Point eindpunt, Color kleur)
        {
            this.startpunt = startpunt;
            this.eindpunt = eindpunt;
            this.kleur = kleur;
        }

        public abstract void Teken(SchetsControl s);
        public abstract void verwijder(SchetsControl s);
        public abstract bool geselecteerd(SchetsControl s, Point p);
    }

    public class TekstObject : TekenObject
    {
        public TekstObject(Point startpunt, Color kleur) : base(startpunt, startpunt, kleur) { }

        public override void Teken(SchetsControl s)
        {

        }
        public override void verwijder(SchetsControl s) { }
        public override bool geselecteerd(SchetsControl s, Point p)
        {
            throw new System.NotImplementedException();
        }
    }

    public class RechthoekObject : TekenObject

    {
        public RechthoekObject(Point beginpunt, Point eindpunt, Color kleur) : base(beginpunt, eindpunt, kleur) { }

        public override void Teken(SchetsControl s)
        {
            
                new RechthoekTool().Bezig(s.MaakBitmapGraphics(), startpunt, eindpunt, new SolidBrush(this.kleur));
        }

        public override void verwijder(SchetsControl s)
        {
            s.getekendeObjecten.Remove(this);
            s.TekenObjecten();
        }

        public override bool geselecteerd(SchetsControl s, Point p)
        {
            int bovenzijde = startpunt.Y;
            if (startpunt.Y > eindpunt.Y) bovenzijde = eindpunt.Y;
            int onderzijde = eindpunt.Y;
            if (startpunt.Y > eindpunt.Y) onderzijde = startpunt.Y;
            int linkerzijde = startpunt.X;
            if (startpunt.X > eindpunt.X) linkerzijde = eindpunt.X;
            int rechterzijde = eindpunt.X;
            if (startpunt.X > eindpunt.X) rechterzijde = startpunt.X;


            if (p.X > linkerzijde - 6 && p.X < rechterzijde - 6 && p.Y > bovenzijde - 6 && p.Y < bovenzijde + 6)
                return true;
            else if (p.X > linkerzijde - 6 && p.X < rechterzijde - 6 && p.Y > onderzijde - 6 && p.Y < onderzijde + 6)
                return true;
            else if (p.Y > bovenzijde - 6 && p.Y < onderzijde - 6 && p.X > linkerzijde - 6 && p.X < linkerzijde + 6)
                return true;
            else if (p.Y > bovenzijde - 6 && p.Y < onderzijde - 6 && p.X > rechterzijde - 6 && p.X < rechterzijde + 6)
                return true;
            else return false;
        }

    }
}
