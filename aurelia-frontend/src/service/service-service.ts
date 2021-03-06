import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IService } from 'domain/IService';
import { IServiceEdit } from 'domain/IServiceEdit';
import { IServiceCreate } from 'domain/IServiceCreate';
import {ICar} from "../domain/ICar";
import {IsInServiceService} from "./isInService-service";
import {IIsInService} from "../domain/IIsInService";
import {IServiceWTHCampaign} from "../domain/IServiceWTHCampaign";
import {BaseService} from "./base-service";

@autoinject
export class ServiceService  extends BaseService<IService> {
    constructor(private appState: AppState, protected httpClient: HttpClient) {
        super('Services', httpClient);
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    private readonly _baseUrl = 'Services';

    async getService(serviceId: string, priceOfService: number): Promise<IFetchResponse<IService>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + serviceId+ '/' + priceOfService, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IService;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getAdminService(serviceId: string): Promise<IFetchResponse<IServiceEdit>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + serviceId, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IServiceEdit;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async createService(Service: IServiceCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .post(this._baseUrl, JSON.stringify(Service), {
                    cache: 'no-store',
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                })

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async updateService(Service: IServiceEdit): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .put(this._baseUrl + '/' + Service.id, JSON.stringify(Service), {
                    cache: 'no-store',
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async updateServiceWTHCampaign(Service: IServiceWTHCampaign): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .put(this._baseUrl + '/' + Service.id, JSON.stringify(Service), {
                    cache: 'no-store',
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async deleteService(id: string): Promise<IFetchResponse<string>> {

        try {
            const response = await this.httpClient
            .delete(this._baseUrl + '/' + id, null, {
                cache: 'no-store',
                headers: {
                    authorization: "Bearer " + this.appState.jwt
                }
            });

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getPriceOfService(car: ICar, serviceId: string): Promise<IFetchResponse<number>> {
        try {
            const response = await this.httpClient
                .post(this._baseUrl + "/ServicePrice/" + serviceId, JSON.stringify(car), {
                    cache: 'no-store',
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                })
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as number;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async createNewIsInService(isInService: IIsInService): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .post('isinservices', JSON.stringify(isInService), {
                    cache: 'no-store',
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                })

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

}
