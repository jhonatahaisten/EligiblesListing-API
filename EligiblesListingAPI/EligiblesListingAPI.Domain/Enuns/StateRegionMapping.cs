namespace EligiblesListingAPI.Domain.Enuns
{
    public static class StateRegionMapping
    {
        private static readonly Dictionary<string, ERegion> _stateRegionMap = new Dictionary<string, ERegion>
        {
            { "ACRE", ERegion.Norte },
            { "ALAGOAS", ERegion.Nordeste },
            { "AMAPÁ", ERegion.Norte },
            { "AMAZONAS", ERegion.Norte },
            { "BAHIA", ERegion.Nordeste },
            { "CEARÁ", ERegion.Nordeste },
            { "DISTRITO FEDERAL", ERegion.CentroOeste },
            { "ESPÍRITO SANTO", ERegion.Sudeste },
            { "GOIÁS", ERegion.CentroOeste },
            { "MARANHÃO", ERegion.Nordeste },
            { "MATO GROSSO", ERegion.CentroOeste },
            { "MATO GROSSO DO SUL", ERegion.CentroOeste },
            { "MINAS GERAIS", ERegion.Sudeste },
            { "PARÁ", ERegion.Norte },
            { "PARAÍBA", ERegion.Nordeste },
            { "PARANÁ", ERegion.Sul },
            { "PERNAMBUCO", ERegion.Nordeste },
            { "PIAUÍ", ERegion.Nordeste },
            { "RIO DE JANEIRO", ERegion.Sudeste },
            { "RIO GRANDE DO NORTE", ERegion.Nordeste },
            { "RIO GRANDE DO SUL", ERegion.Sul },
            { "RONDÔNIA", ERegion.Norte },
            { "RORAIMA", ERegion.Norte },
            { "SANTA CATARINA", ERegion.Sul },
            { "SÃO PAULO", ERegion.Sudeste },
            { "SERGIPE", ERegion.Nordeste },
            { "TOCANTINS", ERegion.Norte }
        };

        public static ERegion GetRegionByState(string state)
        {
            return _stateRegionMap.TryGetValue(state.ToUpper(), out var region) ? region : ERegion.Desconhecido;
        }
    }
}