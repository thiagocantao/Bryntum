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
        "text": "Description",
        "width": 400,
        "flex": 1
    },
    {
        "field": "inicio",
        "text": "Start",
        "width": 140
    },
    {
        "field": "termino",
        "text": "Finished",
        "width": 140
    }
];