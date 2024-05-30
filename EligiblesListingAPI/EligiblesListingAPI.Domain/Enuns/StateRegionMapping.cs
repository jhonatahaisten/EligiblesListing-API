namespace EligiblesListingAPI.Domain.Enuns
{
    public static class StateRegionMapping
    {
        private static readonly Dictionary<string, ERegion> _stateRegionMap = new Dictionary<string, ERegion>
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

        public static ERegion GetRegionByState(string state)
        {
            return _stateRegionMap.TryGetValue(state.ToUpper(), out var region) ? region : ERegion.desconhecido;
        }
    }
}