import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IFetchResponse } from 'types/IFetchResponse';
import { AppState } from 'state/app-state';
import {BaseService} from "./base-service";
import {IScore} from "../domain/IScore";
import {IScoreCreate} from "../domain/IScoreCreate";

@autoinject
export class ScoreService extends BaseService<IScore> {
  constructor(private appState: AppState, protected httpClient: HttpClient) {
    super('scores', httpClient);
    this.httpClient.baseUrl = this.appState.baseUrl;
  }

  private readonly _baseUrl = 'scores';


  async getAll(): Promise<IFetchResponse<IScore[]>> {
    try {
      const response = await this.httpClient
        .fetch(this.apiEndpointUrl, {
          cache: "no-store"
        });
      // happy case
      if (response.ok) {
        const data = (await response.json()) as IScore[];
        return {
          statusCode: response.status,
          data: data
        }
      }

      // something went wrong
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


  async getAllUserScore(): Promise<IFetchResponse<IScore[]>> {
    try {
      const response = await this.httpClient
        .fetch(this.apiEndpointUrl + '/AverageUser', {
          cache: "no-store"
        });
      // happy case
      if (response.ok) {
        const data = (await response.json()) as IScore[];
        return {
          statusCode: response.status,
          data: data
        }
      }

      // something went wrong
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


  async getScore(id: string): Promise<IFetchResponse<IScore>> {
    try {
      const response = await this.httpClient
        .fetch(this._baseUrl + '/' + id, {
          cache: "no-store",
          headers: {
            authorization: "Bearer " + this.appState.jwt
          }
        });

      if (response.status >= 200 && response.status < 300) {
        const data = (await response.json()) as IScore;
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

  async getQuizScore(id: string): Promise<IFetchResponse<number>> {
    console.log(id)
    try {
      const response = await this.httpClient
        .fetch(this._baseUrl + '/Average/' + id, {
          cache: "no-store",
          headers: {
            authorization: "Bearer " + this.appState.jwt
          }
        });

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

    } catch (reason) {
      return {
        statusCode: 0,
        errorMessage: JSON.stringify(reason)
      }
    }
  }

  async updateScore(Score: IScore): Promise<IFetchResponse<string>> {
    try {
      const response = await this.httpClient
        .put(this._baseUrl + '/' + Score.id, JSON.stringify(Score), {
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


  async createScore(Score: IScoreCreate): Promise<IFetchResponse<string>> {
    try {
      const response = await this.httpClient
        .post(this._baseUrl, JSON.stringify(Score), {
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

  async deleteScore(id: string): Promise<IFetchResponse<string>> {

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


}
