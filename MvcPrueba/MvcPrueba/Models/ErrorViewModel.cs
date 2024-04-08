namespace MvcPrueba.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

/*
@foreach (var rating in ratings)
                {
                    <div class="container-fluid">
                        <div class="mt-2">
                            <a class="text-decoration-none" asp-action="PanelUser" asp-controller="Users" asp-route-idUser="@rating.IdUsuario">
                                <div class="card rounded-lg shadow-sm text-light" style="background-color: #31304c; border: none;">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-5 col-md-4 d-flex align-items-center">
                                                <img class="img-fluid rounded-circle col-6 col-md-10 mx-auto d-block"
                                                     src="@rating.FotoUser" />
                                            </div>
                                            <div class="col-sm-7 col-md-8 mt-sm-0 mt-3 d-flex flex-column align-items-center align-items-sm-start justify-content-sm-center">
                                                <span class="fs-5 fw-bold">@rating.NombreUser</span>
                                                <span class="fs-5">@rating.Nota</span>
                                            </div>
                                        </div>
                                        <h5 class="card-title fw-bold mt-4 mb-1">
                                            @rating.Titulo
                                        </h5>
                                        <small style="opacity: 60%">14/03/2024</small>
                                        <p class="card-text mt-2">
                                            @rating.Comentario
                                        </p>
                                        <div class="row mt-3">
                                            <div class="d-flex justify-content-end">
                                                <svg style="width: 25px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="w-5 h-5">
                                                    <path d="m9.653 16.915-.005-.003-.019-.01a20.759 20.759 0 0 1-1.162-.682 22.045 22.045 0 0 1-2.582-1.9C4.045 12.733 2 10.352 2 7.5a4.5 4.5 0 0 1 8-2.828A4.5 4.5 0 0 1 18 7.5c0 2.852-2.044 5.233-3.885 6.82a22.049 22.049 0 0 1-3.744 2.582l-.019.01-.005.003h-.002a.739.739 0 0 1-.69.001l-.002-.001Z" />
                                                </svg>
                                                <span class="fw-bold ms-1">@rating.LikesTotales</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                }
 */