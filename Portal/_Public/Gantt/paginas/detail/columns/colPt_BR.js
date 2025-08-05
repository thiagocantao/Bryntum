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
        filterable: true,
        htmlEncode: false, // allow to use HTML code
        hidden: true
    },
  {
    "field": "nomeTarefa",
    "text": "Tarefa",
      "width": 450,
      
      filterable: true,
      htmlEncode: false, // allow to use HTML code
      renderer: ({ record, row }) => {
          return record.nomeTarefa;
      }
  },    
  {
    "field": "inicioLb",
     "text": "Início LB",
     "width": 140,
      readonly: true
  },
  {
    "field": "terminoLb",
    "text": "Término LB",
      "width": 140,
      
  },
  {
    "field": "previstoStr",
    "type": "column",
      "text": "Previsto",
      "width": 90
  },
  {
    "field": "realizado",
    "type": "column",
    "text": "Realizado",
      
      "width": 90,
      readonly: true
  },
  {
    "field": "pesoLb",
    "text": "Peso LB",
      
      "width": 90,
      readonly: true
  },
  {
    "field": "peso",
    "text": "% Peso",
      
      "width": 90,
      readonly: true
  },
  {
    "field": "duracaoStr",
    "text": "Duração (d)",
      
      "width": 110,
      readonly: true
  },
  {
    "field": "trabalho",
    "text": "Trabalho (h)",
      
      "width": 110,
      readonly: true
  },
  {
    "field": "inicio",
    "text": "Início",
      "width": 140,
      readonly: true
  },
  {
    "field": "termino",
    "text": "Término",
      "width": 140,
      readonly: true
  },
  {
    "field": "terminoReal",
    "text": "Término Real",
      "width": 140,
      readonly: true
    },
    {
        'field': 'isCaminhoCriticoStr',
        'text': 'Crítico?',
        'width': 100,
        readonly: true
    },
    {
        "field": "isMarcoStr",
        "text": "Marco ?",
        
        "width": 90,
        readonly: true
    },
    {
        "field": "isAtrasoStr",
        "text": "Atrasada ?",
        
        "width": 110,
        readonly: true
    },
    {
        "field": "recurso",
        "text": "Recurso",
        "width": 500,
        readonly: true
    }
];
