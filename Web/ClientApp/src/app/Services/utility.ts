import { HttpResponseBase, HttpResponse, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import {Injectable} from '@angular/core';
import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { environment } from 'src/environments/environment';




Injectable();
export class Utility {

    public static readonly captionAndMessageSeparator = ':';
    public static readonly noNetworkMessageCaption = 'No Network';
    public static readonly noNetworkMessageDetail = 'The server cannot be reached';
    public static readonly accessDeniedMessageCaption = 'Access Denied!';
    public static readonly accessDeniedMessageDetail = '';
    public static getGetOption(): any {
        return  {
            observe: 'response',
            responseType: 'blob',
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        };
    }
    public static getPostOption(content: any): any {
        return  {
            body: content,
            observe: 'response',
            responseType: 'blob',
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                Accept: 'application/json'
            })
        };
    }
    public static baseUrl() {
        const base = environment.baseUrl;
        return base.replace(/\/$/, '');
    }

    public static getResponseBody(response: HttpResponseBase) {
        if (response instanceof HttpResponse) {
            return response.body;
        }
        if (response instanceof HttpErrorResponse) {
            return response.error || response.message || response.statusText;
        }
    }


    public static checkNoNetwork(response: HttpResponseBase) {
        if (response instanceof HttpResponseBase) {
            return response.status === 0;
        }

        return false;
    }

    public static checkAccessDenied(response: HttpResponseBase) {
        if (response instanceof HttpResponseBase) {
            return response.status === 403;
        }

        return false;
    }

    public static checkNotFound(response: HttpResponseBase) {
        if (response instanceof HttpResponseBase) {
            return response.status === 404;
        }

        return false;
    }

    public static checkIsLocalHost(url: string, base?: string) {
        if (url) {
            const location = new URL(url, base);
            return location.hostname === 'localhost' || location.hostname === '127.0.0.1';
        }

        return false;
    }



    public static getQueryParamsFromString(paramString: string) {

        if (!paramString) {
            return null;
        }

        const params: { [key: string]: string } = {};

        for (const param of paramString.split('&')) {
            const keyValue = Utility.splitInTwo(param, '=');
            params[keyValue.firstPart] = keyValue.secondPart;
        }

        return params;
    }


    public static splitInTwo(text: string, separator: string): { firstPart: string, secondPart: string } {
        const separatorIndex = text.indexOf(separator);

        if (separatorIndex === -1) {
            return { firstPart: text, secondPart: null };
        }

        const part1 = text.substr(0, separatorIndex).trim();
        const part2 = text.substr(separatorIndex + 1).trim();

        return { firstPart: part1, secondPart: part2 };
    }

    public static JSonTryParse(value: string) {
        try {
            return JSON.parse(value);
        } catch (e) {
            if (value === 'undefined') {
                return void 0;
            }

            return value;
        }
    }
    public static getHttpResponseMessage(data: HttpResponseBase | any): string[] {

        const responses: string[] = [];

        if (data instanceof HttpResponseBase) {

            if (this.checkNoNetwork(data)) {
                responses.push(`${this.noNetworkMessageCaption}${this.captionAndMessageSeparator} ${this.noNetworkMessageDetail}`);
            } else {
                const responseObject = this.getResponseBody(data);

                if (responseObject && (typeof responseObject === 'object' || responseObject instanceof Object)) {

                    for (const key in responseObject) {
                        if (key) {
                            responses.push(`${key}${this.captionAndMessageSeparator} ${responseObject[key]}`);
                        } else if (responseObject[key]) {
                            responses.push(responseObject[key].toString());
                             }
                    }
                }
            }

            if (!responses.length && this.getResponseBody(data)) {
                responses.push(`${data.statusText}: ${this.getResponseBody(data).toString()}`);
            }
        }

        if (!responses.length) {
            responses.push(data.toString());
        }

        if (this.checkAccessDenied(data)) {
            responses.splice(0, 0,
                `${this.accessDeniedMessageCaption}${this.captionAndMessageSeparator} ${this.accessDeniedMessageDetail}`);
        }


        return responses;
    }

    public static findHttpResponseMessage(
        messageToFind: string, data: HttpResponse<any> | any, seachInCaptionOnly = true, includeCaptionInResult = false): string {

        const searchString = messageToFind.toLowerCase();
        const httpMessages = this.getHttpResponseMessage(data);

        for (const message of httpMessages) {
            const fullMessage = Utility.splitInTwo(message, this.captionAndMessageSeparator);

            // tslint:disable-next-line:triple-equals
            if (fullMessage.firstPart && fullMessage.firstPart.toLowerCase().indexOf(searchString) != -1) {
                return includeCaptionInResult ? message : fullMessage.secondPart || fullMessage.firstPart;
            }
        }

        if (!seachInCaptionOnly) {
            for (const message of httpMessages) {

                // tslint:disable-next-line:triple-equals
                if (message.toLowerCase().indexOf(searchString) != -1) {
                    if (includeCaptionInResult) {
                        return message;
                    } else {
                        const fullMessage = Utility.splitInTwo(message, this.captionAndMessageSeparator);
                        return fullMessage.secondPart || fullMessage.firstPart;
                    }
                }
            }
        }

        return null;
    }

}


