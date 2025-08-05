var columns = [
    {
        "type": "column",
        "field": "edtcode",
        "text": "#",
        "width": 50
    },        
  {
    "type": "name",
      "text": "Tarefa", //OK
      "width": 450,
      filterable: true,
      htmlEncode: true, // allow to use HTML code
      renderer: ({ record, row }) => {
          return record.name;
      }
  },    
  {
    "field": "inicioLb",
     "text": "Início LB", //OK
      "width": 140,
      filterable: true,
      readOnly: true
  },
  {
    "field": "terminoLb",
      "text": "Término LB",//OK
      filterable: true,
      "width": 140,
      
    },
    {
        "field": "situacao",// Não tem
        "text": "Situação",
        filterable: true,
        "width": 140
    },
  {
    "field": "previstoStr", //OK
    "type": "column",
      "text": "Previsto", 
      filterable: true,
      "width": 140
  },
  {
    "field": "realizado", //OK
    "type": "column",
    "text": "Concluído", 
      "width": 140,
      readOnly: true
    },
    {
        "field": "tipoTarefa", //Não tem
        "text": "Tipo da tarefa",
        "width": 140,
        readOnly: true
    },
    {
        "field": "duracaoStr", //OK
        "text": "Duração (d)",
        "width": 140,
        readOnly: true
    },
  {
    "field": "pesoLb", //OK
    "text": "Peso LB",
      "width": 140,
      readOnly: true
  },
  {
    "field": "peso", //OK
    "text": "% Peso",
      "width": 140,
      readOnly: true
  },
  {
    "field": "inicio", //OK
    "text": "Início",
      "width": 140,
      readOnly: true
  },
  {
    "field": "termino", //OK
    "text": "Término",
      "width": 140,
      readOnly: true
    },

    {
        "field": "predecessoras", //Não tem
        "text": "Predecessoras",
        "width": 140,
        readOnly: true
    },

    {
        "field": "recurso", //Dúvida
        "text": "Recursos",
        "width": 140,
        readOnly: true
    },
    {
        "field": "trabalho", //OK 
        "text": "Trabalho (Hrs)",
        "width": 140,
        readOnly: true
    },
    {
        "field": "custo", //OK
        "text": "Custo (R$)",
        "width": 140,
        readOnly: true
    },

    {
        "field": "duracao", //OK
        "text": "Duração Real",
        "width": 140,
        readOnly: true
    },

    {
        "field": "inicio", //OK
        "text": "Início Real",
        "width": 140,
        readOnly: true
    },
    {
        "field": "terminoReal", //OK
        "text": "Término Real",
        "width": 140,
        readOnly: true
    },
    {
        "field": "trabalhoReal", //Não tem
        "text": "Trabalho Real (Hrs)",
        "width": 140,
        readOnly: true
    },
    {
        "field": "custoReal", //Não tem
        "text": "Custo Real(R$)",
        "width": 140,
        readOnly: true
    },

    {
        "field": "duracaoLB", //OK
        "text": "Duração LB",
        "width": 140,
        readOnly: true
    },
    {
        "field": "trabalhoLB",//Não tem
        "text": "Trabalho LB",
        "width": 140,
        readOnly: true
    },
    {
        "field": "custoLB", //Não tem
        "text": "Custo LB",
        "width": 140,
        readOnly: true
    },
    
];
