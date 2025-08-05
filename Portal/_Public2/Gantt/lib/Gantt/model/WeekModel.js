import Model from '../../Common/data/Model.js';

export default class Week extends Model {
    // static get fields() {
    //     return [
    //         { name : 'id' },
    //         { name : 'name', type : 'string' },
    //         { name : 'startDate', type : 'date' },
    //         { name : 'endDate', type : 'date' },
    //         { name : 'mainDay' }, // type : Gantt.model.CalendarDay
    //         { name : 'weekAvailability' }
    //     ];
    // }
    //
    // set(field, value) {
    //     if (field === 'name') {
    //         // rename every CalendarDay instance embedded
    //         this.weekAvailability.concat(this.mainDay).forEach(weekDay => {
    //             if (weekDay) {
    //                 weekDay.name = value;
    //             }
    //         });
    //     }
    //
    //     super.set(field, value);
    // }
}
