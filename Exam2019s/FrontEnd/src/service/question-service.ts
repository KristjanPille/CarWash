import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IFetchResponse } from 'types/IFetchResponse';
import { AppState } from 'state/app-state';
import {BaseService} from "./base-service";
import {IQuestion} from "../domain/IQuestion";
import {IQuestionCreate} from "../domain/IQuestionCreate";

@autoinject
export class QuestionService extends BaseService<IQuestion> {
    constructor(private appState: AppState, protected httpClient: HttpClient) {
        super('Questions', httpClient);
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    private readonly _baseUrl = 'Questions';


    async getAll(): Promise<IFetchResponse<IQuestion[]>> {
        try {
            const response = await this.httpClient
                .fetch(this.apiEndpointUrl, {
                    cache: "no-store"
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IQuestion[];
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

  async getQuizSpecificQuestions(quizId: string): Promise<IFetchResponse<IQuestion[]>> {
    try {
      const response = await this.httpClient
        .fetch(this.apiEndpointUrl + '/quiz/' + quizId, {
          cache: "no-store"
        });
      // happy case
      if (response.ok) {
        const data = (await response.json()) as IQuestion[];
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


    async getQuestion(id: string): Promise<IFetchResponse<IQuestion>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + id, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IQuestion;
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

    async updateQuestion(question: IQuestion): Promise<IFetchResponse<string>> {
      console.log(question)
        try {
            const response = await this.httpClient
                .put(this._baseUrl + '/' + question.id, JSON.stringify(question), {
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


    async createQuestion(question: IQuestionCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .post(this._baseUrl, JSON.stringify(question), {
                    cache: 'no-store',
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                })

            if (response.status >= 200 && response.status < 300) {
              const data = (await response.json()) as string;
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

    async deleteQuestion(id: string): Promise<IFetchResponse<string>> {

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
