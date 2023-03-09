export default class DateConverter {
    public getDateString(timeStamp: number): string {
        var date = new Date(timeStamp * 1000);
        return date.toLocaleDateString();
    }

    public getFullDateString(timeStamp: number): string {
        var date = new Date(timeStamp * 1000);
        return date.toLocaleString();
    }
}

export const dateConverter = new DateConverter();
