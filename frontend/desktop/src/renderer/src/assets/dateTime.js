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
