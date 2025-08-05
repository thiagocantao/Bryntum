var columns = [
    {        
        "type": "note",        
        "field" : "icone",
        "text": "",
        "width": 70,        
        align: "center",
        filterable: false,
        htmlEncode: false, // allow to use HTML code
        renderer({ value }) {
            return value === true ? value.icone : '';
        }
    },    
    {
        "type": "name",
        "field": "descricao",
        "text": "Descrição",
        "width": 400
    },
    {
        "field": "inicio",
        "text": "Início",
        "width": 140
    },
    {
        "field": "termino",
        "text": "Término",
        "width": 140
    },
    {
        "field": "gerente",
        "text": "Gerente",
        "width": 140
    },
    {
        "field": "situacao",
        "text": "Situação",
        "width": 140,
        filterable: {
            filterField: {
                type: 'combo',
                items: ['Em Execução',
                        'Cancelado',
                        'Suspenso',
                        'Encerrado',
                        'Aguardando Termo de Abertura',
                        'Aguardando Suspensão/ Encerramento',
                        'Em Planejamento']
            }
        }
    }
];
