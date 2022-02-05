export const timeFormat = (time) => {
    if(time){
    var arr = time.split('T')
    var dateArr = arr[0].split('-')
    var newtime = arr[1]
    const date = dateArr[2]+'.'+dateArr[1]+'.'+dateArr[0]
    const dateTime = date+' '+newtime
    return dateTime }
}

export const dateFormat = (date) => {
    var dateArr = date.split('-')
    const Date = dateArr[2]+'.'+dateArr[1]+'.'+dateArr[0]
    return Date
}

export const dateFormat2 = (date) => {
    var dateArr_ = date.split('T');
    var dateArr = dateArr_[0].split("-");
    const Date = dateArr[2]+'.'+dateArr[1]+'.'+dateArr[0]
    return Date
}

export const dateFormat3 = (date) => {
    var dateArr = date.split('.')
    const Date = dateArr[1]+'.'+dateArr[0]+'.'+dateArr[2]
    return Date
}

export const dateFormat4 = (date) => {
    console.log(date)
    var dateArr_ = date.split('T');
    var dateArr = dateArr_[1].split("-");
    const Date = dateArr[0]+':'+dateArr[1]+':'+dateArr[2]
    return Date
}

export const strToDate = (date) => {
    // gg.aa.yyyy
    const dateArr = date.split('.');
    var dateObject = new Date(dateArr[2],dateArr[1],dateArr[0])
    return dateObject;
}
