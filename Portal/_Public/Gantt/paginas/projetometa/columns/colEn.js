var columns = [
    {
        "type": "note",
        "field": "icone",
        "text": "",
        "width": 70,
        align: "center",
        filterable: false,
        htmlEncode: false, // allow to use HTML code
        renderer({ value }) {
            return value === true ? value.icone : '';
        }
    },
    //{
    //    field: 'approved',
    //    text: 'Approved',
    //    htmlEncode: false, // allow to use HTML code
    //    renderer({ value }) {
    //        return value === true ? '<b>Yes</b>' : '<i>No</i>';
    //    }
    //},
    {
        "type": "name",
        "field": "descricao",
        "text": "Description",
        "width": 400
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
    },
    {
        "field": "gerente",
        "text": "Manager",
        "width": 140
    },
    {
        "field": "situacao",
        "text": "Status",
        "width": 140
    }
];