namespace EligiblesListingAPI.Domain.Enums
{
    public static class StateRegionMapping
    {
        public static readonly Dictionary<string, ERegion> stateRegionMap = new()
        {
            { "ACRE", ERegion.norte },
            { "ALAGOAS", ERegion.nordeste },
            { "AMAPÁ", ERegion.norte },
            { "AMAZONAS", ERegion.norte },
            { "BAHIA", ERegion.nordeste },
            { "CEARÁ", ERegion.nordeste },
            { "DISTRITO FEDERAL", ERegion.centroOeste },
            { "ESPÍRITO SANTO", ERegion.sudeste },
            { "GOIÁS", ERegion.centroOeste },
            { "MARANHÃO", ERegion.nordeste },
            { "MATO GROSSO", ERegion.centroOeste },
            { "MATO GROSSO DO SUL", ERegion.centroOeste },
            { "MINAS GERAIS", ERegion.sudeste },
            { "PARÁ", ERegion.norte },
            { "PARAÍBA", ERegion.nordeste },
            { "PARANÁ", ERegion.sul },
            { "PERNAMBUCO", ERegion.nordeste },
            { "PIAUÍ", ERegion.nordeste },
            { "RIO DE JANEIRO", ERegion.sudeste },
            { "RIO GRANDE DO NORTE", ERegion.nordeste },
            { "RIO GRANDE DO SUL", ERegion.sul },
            { "RONDÔNIA", ERegion.norte },
            { "RORAIMA", ERegion.norte },
            { "SANTA CATARINA", ERegion.sul },
            { "SÃO PAULO", ERegion.sudeste },
            { "SERGIPE", ERegion.nordeste },
            { "TOCANTINS", ERegion.norte }
        };       
    }
}