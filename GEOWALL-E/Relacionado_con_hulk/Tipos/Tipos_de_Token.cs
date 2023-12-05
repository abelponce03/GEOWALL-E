namespace GEOWALL_E
{
    // Esta es la clase que le va a dar las propiedades al token
    enum Tipo_De_Token
    {
        //tokens
        Numero,
        String,
        Comillas,
        coma,
        Suma,
        Resta,
        Producto,
        Division,
        Potenciacion,
        Parentesis_Abierto,
        Parentesis_Cerrado,
        Bang,
        Igual,
        Implicacion,
        AmpersandAmpersand,
        PipePipe,
        Menor_que,
        Menor_igual_que,
        Mayor_que,
        Mayor_igual_que,
        IgualIgual,
        Bang_Igual,
        Espacio,
        Malo,
        punto_y_coma,
        Final,
        Identificador,
        Declaracion_Funcion,
        LLamada_Funcion,
        concatenacion,
        resto,
        salto_de_linea,
        la_nada,

        //Keywords
        if_Keyword,
        then_Keyword,
        else_Keyword,
        print_Keyword,
        let_Keyword,
        in_Keyword,
        sen_Keyword,
        cos_Keyword,
        PI_Keyword,
        function_Keyword,
        logaritmo_Keyword,
        clean_keyword,


        //Expresiones
        Parentesis,
        Expresion_Unaria,
        Expresion_Binaria,
        Literal,
        Variable,
        if_Expresion,
        else_Expresion,
        Print_Expresion,
        let_in_Expresion,
        let_Expresion,
        in_Expresion,
        sen_Expresion,
        cos_Expresion,
        function_Expresion,
        parametros_Expresion,
        logaritmo_Expresion,
        clean_Expresion,

        //Geometria expresiones
        point_Expresion,
        circle_Expresion,
        arc_Expresion,
        line_Expresion,
        ray_Expresion,
        segment_Expresion,
        measure_Expresion,

        draw_Expresion,

        //Geometria keywords
        arc_Keyword,
        circle_Keyword,
        line_Keyword,
        measure_Keyword,
        point_Keyword,
        ray_Keyword,
        segment_Keyword,

        draw_Keyword,

    }
}
