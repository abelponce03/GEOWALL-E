namespace GEOWALL_E
{
    class Keyword
    {
        public static Tipo_De_Token Tipo(string texto)
        {
            switch (texto)
            {
                case "clean":
                    return Tipo_De_Token.clean_keyword;
                case "then":
                    return Tipo_De_Token.then_Keyword;
                case "if":
                    return Tipo_De_Token.if_Keyword;
                case "else":
                    return Tipo_De_Token.else_Keyword;
                case "print":
                    return Tipo_De_Token.print_Keyword;
                case "let":
                    return Tipo_De_Token.let_Keyword;
                case "in":
                    return Tipo_De_Token.in_Keyword;
                case "sen":
                    return Tipo_De_Token.sen_Keyword;
                case "cos":
                    return Tipo_De_Token.cos_Keyword;
                case "PI":
                    return Tipo_De_Token.PI_Keyword;
                case "function":
                    return Tipo_De_Token.function_Keyword;
                case "log":
                    return Tipo_De_Token.logaritmo_Keyword;

                //Geometria keywords
                case "arc":
                    return Tipo_De_Token.arc_Keyword;
                case "circle":
                    return Tipo_De_Token.circle_Keyword;
                case "line":
                    return Tipo_De_Token.line_Keyword;
                case "measure":
                    return Tipo_De_Token.measure_Keyword;
                case "point":
                    return Tipo_De_Token.point_Keyword;
                case "ray":
                    return Tipo_De_Token.ray_Keyword;
                case "segment":
                    return Tipo_De_Token.segment_Keyword;
                case "draw":
                    return Tipo_De_Token.draw_Keyword;
                case "undefined":
                    return Tipo_De_Token.undefined_Keyword;
                case "randoms":
                    return Tipo_De_Token.randoms_Keyword;
                case "samples":
                    return Tipo_De_Token.samples_Keyword;
                case "count":



                default:
                    return Tipo_De_Token.Identificador;
            }
        }
    }
}
