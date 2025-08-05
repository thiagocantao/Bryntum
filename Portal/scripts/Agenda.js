function OnMenuClick(s, e) {
    if (e.item.GetItemCount() <= 0) {
        if (e.item.name == "ExportAppointment")
            calendarioAgenda.SendPostBack("EXPORTAPT|");
        else
            calendarioAgenda.RaiseCallback("MNUAPT|" + e.item.name);
    }
}