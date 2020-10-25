export const convertToPercentage = (num: number): number => num / 100

export const formValidation = {
  addServerErrors: <T>(
    errors: { [P in keyof T]?: string[] },
    setError: (fieldName: keyof T, error: { type: string; message: string }) => void
  ): void =>
    Object.keys(errors).forEach(key => {
      setError(key as keyof T, {
        type: 'server',
        message: errors[key as keyof T]!.join('. '), // eslint-disable-line @typescript-eslint/no-non-null-assertion
      })
    }),
}
