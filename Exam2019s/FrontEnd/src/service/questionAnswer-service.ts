import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IFetchResponse } from 'types/IFetchResponse';
import { AppState } from 'state/app-state';
import {BaseService} from "./base-service";
import {IQuestionAnswer} from "../domain/IQuestionAnswer";
import {IQuestionAnswerCreate} from "../domain/IQuestionAnswerCreate";

@autoinject
export class QuestionAnswerService extends BaseService<IQuestionAnswer> {
  constructor(private appState: AppState, protected httpClient: HttpClient) {
    super('QuestionAnswers', httpClient);
    this.httpClient.baseUrl = this.appState.baseUrl;
  }

  private readonly _baseUrl = 'QuestionAnswers';


  async getAll(): Promise<IFetchResponse<IQuestionAnswer[]>> {
    try {
      const response = await this.httpClient
        .fetch(this.apiEndpointUrl, {
          cache: "no-store"
        });
      // happy case
      if (response.ok) {
        const data = (await response.json()) as IQuestionAnswer[];
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


  async getQuestionAnswer(id: string): Promise<IFetchResponse<IQuestionAnswer>> {
    try {
      const response = await this.httpClient
        .fetch(this._baseUrl + '/' + id, {
          cache: "no-store",
          headers: {
            authorization: "Bearer " + this.appState.jwt
          }
        });

      if (response.status >= 200 && response.status < 300) {
        const data = (await response.json()) as IQuestionAnswer;
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

  async updateQuestionAnswer(questionAnswer: IQuestionAnswer): Promise<IFetchResponse<string>> {
    try {
      const response = await this.httpClient
        .put(this._baseUrl + '/' + questionAnswer.id, JSON.stringify(questionAnswer), {
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


  async createQuestionAnswer(questionAnswerCreate: IQuestionAnswerCreate): Promise<IFetchResponse<string>> {
    try {
      const response = await this.httpClient
        .post(this._baseUrl, JSON.stringify(questionAnswerCreate), {
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

  async deleteQuestionAnswer(id: string): Promise<IFetchResponse<string>> {

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
