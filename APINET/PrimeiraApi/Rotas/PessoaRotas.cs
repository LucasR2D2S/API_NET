using PrimeiraApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace PrimeiraApi.Rotas
{
    public static class PessoaRotas
    {
        public static List<Pessoa> Pessoas =
        [
            new(Guid.NewGuid(), "Lucas"),
            new(Guid.NewGuid(), "Yuri Alberto"),
            new(Guid.NewGuid(), "Thor")
        ];

        public static void MapPessoaRotas(this WebApplication app) 
        {
            app.MapGet("/pessoas", () => Pessoas );

            app.MapGet("/pessoa/{nome}", 
                (string nome)=>  Pessoas.Find(x => x.Nome == nome));

            app.MapPost("/pessoas", 
                (HttpContext request, Pessoa pessoa) =>
                {
                    //if (pessoa.Nome != "Lucas")
                    //    return Results.BadRequest(new {message = "Erro pq não é Lucas!"});

                    var nome = request.Request.Query["name"]
;
                    Pessoas.Add(pessoa);
                    return Results.Ok(pessoa);

                });

            app.MapPut("/pessoas/{id:guid}", (Guid id, Pessoa pessoa) =>
                {
                    var encontrado = Pessoas.Find(x => x.Id == id);

                    if (encontrado == null)
                        return Results.NotFound();

                    encontrado.Nome = pessoa.Nome;
                    
                    return Results.Ok(encontrado);
                });

            
        }

    }
}
