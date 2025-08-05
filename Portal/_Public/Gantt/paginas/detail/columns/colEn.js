var columns = [
    {
        "type": "column",
        "field": "edtcode",
        "text": "#",
        "width": 50
    },    
    {
        "type": "name",
        "text": "",
        filterable: false,
        htmlEncode: false, // allow to use HTML code
        hidden: true
    },
    {
        'type': 'name',
        'text': 'Task',
        'width': 450,
         filterable: false,
         htmlEncode: false, // allow to use HTML code
         renderer: ({ record, row }) => {
            return record.nomeTarefa;
         }
    },    
    {
        "field": "inicioLb",
        'text': 'LB Start',
        'width': 140
    },
    {
        "field": "terminoLb",
        'text': 'LB Finish',
        'width': 140
    },
    {
        "field": "previstoStr",
        "type": "column",
        'text': 'Predicted',
        'flex': 1
    },
    {
        "field": "realizado",
        "type": "column",
        'text': 'Realized',
        'flex': 1
    },
    {
        'field': 'pesoLb',
        'text': 'LB Weight',
        'flex': 1
    },
    {
        'field': 'peso',
        'text': '% Weight',
        'flex': 1
    },
    {
        'field': 'duracaoStr',
        'text': 'Duration (d)',
        'flex': 1
    },
    {
        'field': 'trabalho',
        'text': 'Job (h)',
        'flex': 1
    },
    {
        'field': 'inicio',
        'text': 'Start',
        'width': 140
    },
    {
        'field': 'termino',
        'text': 'Finish',        
        'width': 140
    },
    {
        'field': 'terminoReal',
        'text': 'Real Finish',        
        'width': 140
    },
    {
        'field': 'isCaminhoCriticoStr',
        'text': 'Critical?',
        'width': 100
    },
    {
        "field": "isMarcoStr",
        "text": "Milestone ?",
        "flex": 1
    },
    {
        "field": "isAtrasoStr",
        "text": "Is the task late?",
        "flex": 1
    },
    {
        "field": "recurso",
        "text": "Resource",
        "width": 500
    }
];