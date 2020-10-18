const expiryMilliseconds = 86400000 // 24 hours
const ACCESS_TOKENS_KEY = 'accessTokens'

type AccessToken = {
  dateTime: number
  channel: string
  value: string
}

const getActiveTokens = () => {
  const currentTokens = JSON.parse(localStorage.getItem(ACCESS_TOKENS_KEY) || '[]') as AccessToken[]
  const filteredTokens = filterExpiredAccessTokens(currentTokens)

  if (currentTokens.length !== filteredTokens.length)
    localStorage.setItem(ACCESS_TOKENS_KEY, JSON.stringify(filteredTokens))

  return filteredTokens
}

const filterExpiredAccessTokens = (accessTokens: AccessToken[]) => {
  const currentDateTime = Date.now()
  return accessTokens.reduce<AccessToken[]>((accessTokens, accessToken) => {
    if (accessToken.dateTime > currentDateTime - expiryMilliseconds) {
      return [...accessTokens, accessToken]
    }
    return accessTokens
  }, [])
}

const upsertAccessToken = (channel: string, accessToken: string): void => {
  const accessTokens = getActiveTokens()

  // Remove existing token for that channel
  const filteredTokens = accessTokens.reduce<AccessToken[]>((accessTokens, accessToken) => {
    if (accessToken.channel !== channel) {
      return [...accessTokens, accessToken]
    }
    return accessTokens
  }, [])

  localStorage.setItem(
    ACCESS_TOKENS_KEY,
    JSON.stringify([...filteredTokens, { dateTime: Date.now(), channel, value: accessToken }])
  )
}

const getAccessToken = (channel: string): AccessToken | undefined =>
  getActiveTokens().find(token => token.channel === channel)

export const tokenRepository = { upsertAccessToken, getAccessToken }
