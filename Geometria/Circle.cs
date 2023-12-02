
namespace Hulk;
class Circle: Expresion
{
    public Circle(string identificador, Expresion punto, object radio)
    {
        Identificador = identificador;
        Punto = punto;
        Radio = radio; 
    }
    public override Tipo_De_Token Tipo => Tipo_De_Token.circle_Expresion;
    public string Identificador {get;}
    public Expresion Punto {get;}
    public object Radio {get;}
}