using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EligiblesListingAPI.Domain.Enuns
{

    public enum ERegion
    {
        Norte,
        Nordeste,
        CentroOeste,
        Sudeste,
        Sul,
        Desconhecido
    }

 
    public enum EType
    {
        laborious,
        normal,
        special
    }
}