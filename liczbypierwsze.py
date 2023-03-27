input = int(input())
liczbapierwsza = "tak"
i = 2
while i*i < input:
    if input % i == 0:
        liczbapierwsza = "nie"
        break
    else:
        i += 1
print(liczbapierwsza)