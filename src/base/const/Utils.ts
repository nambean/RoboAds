export function isValidCookie(cookie: string): boolean {
    const cookieList = cookie.trim().split(';');
    const cookieKey = cookieList.map(x => x.trim().split('=')[0]);
    return [ 'datr', 'sb', 'c_user', 'xs', 'fr' ].every(value => cookieKey.includes(value));
}

export function isValidIP(ipAddress: string): boolean {
    // Biểu thức chính quy để kiểm tra định dạng địa chỉ IP
    const ipRegex = /^([0-9]{1,3}\.){3}[0-9]{1,3}$/;

    // Kiểm tra xem địa chỉ IP có khớp với biểu thức chính quy hay không
    return ipRegex.test(ipAddress);
}
