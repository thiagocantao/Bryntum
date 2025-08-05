export default class DateHelper {

    static parse (dateString : string | Date, format? : string) : Date

    static add(date : string | Date, amount : number, unit? : string) : Date

    static normalizeUnit(string) : string
}
